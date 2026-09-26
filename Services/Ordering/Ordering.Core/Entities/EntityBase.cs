using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public string? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Updatedby { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
