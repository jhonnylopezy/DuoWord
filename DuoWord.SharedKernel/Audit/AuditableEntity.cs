using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoWord.SharedKernel.Audit
{
    public class AuditableEntity : EntityBase, IAuditableEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        [NotMapped]
        public IEnumerable<IAuditEntity>? AuditEntities { get; set; }
        
    }
}
