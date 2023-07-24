using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm.Core.Entities.Base
{

    public abstract class EntityBase : ISoftDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid ExternalId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModifiedTime { get; set; }

        public bool IsDeleted { get; set; }

        public EntityBase()
        {
            this.CreationTime = DateTime.UtcNow;
            this.ExternalId = Guid.NewGuid();
        }
    }
}
