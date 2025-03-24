using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoWord.SharedKernel
{
    public abstract class EntityBase
    {
        [Key]
        [Column("id")]
        public Guid Id { get; protected set; }
    
        private List<DomainEventBase> _domainEvents =new();
    }
}
