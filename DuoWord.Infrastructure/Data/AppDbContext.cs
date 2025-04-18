﻿using DuoWord.SharedKernel;
using DuoWord.SharedKernel.Audit;
using DuoWord.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DuoWord.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        private readonly IDomainEventDispatcher? _dispatcher;
        private readonly IHttpContextAccessor? _httpContextAccessor;


        public AppDbContext(DbContextOptions<AppDbContext> options,
          IDomainEventDispatcher? dispatcher, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _dispatcher = dispatcher;
            _httpContextAccessor = httpContextAccessor;
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //User For Audits
        public DbSet<Audit> Audits { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //User For Audits
            var auditEntries = OnBeforeSaveChanges();
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //User For Audits
            await OnAfterSaveChanges(auditEntries);
            //User For Domaint Events
            await UseDomainEvents();

            return result;
        }

        public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();

        private async Task UseDomainEvents()
        {
            // ignore events if no dispatcher provided
            if (_dispatcher == null) return;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
        }
        private void SetAuditableEntityFields()
        {
            string userName = "Default";
            if (_httpContextAccessor is not null
                && _httpContextAccessor.HttpContext is not null
                && _httpContextAccessor.HttpContext.User is not null
                && _httpContextAccessor.HttpContext.User.Identity is not null
                && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                userName = _httpContextAccessor.HttpContext!.User.Identity!.Name ?? "Default";

            }

            foreach (var entry in ChangeTracker
                         .Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userName;
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = userName;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            SetAuditableEntityFields();
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State is EntityState.Detached or EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName()!
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {

                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }



            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => !_.HasTemporaryProperties).ToList();
        }
        private Task OnAfterSaveChanges(List<AuditEntry>? auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                Audits.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }
    }
}
