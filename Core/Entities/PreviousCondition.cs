using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PreviousCondition
    {
        public int Id { get; set; }

        public string NameCondition { get; set; } = null!;

        public virtual ICollection<Epp> Epps { get; set; } = new List<Epp>();
    }
}