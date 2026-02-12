using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ReasonRequest
    {
        public int Id { get; set; }

        public string NameReason { get; set; } = null!;

        public virtual ICollection<Epp> Epps { get; set; } = new List<Epp>();
    }
}