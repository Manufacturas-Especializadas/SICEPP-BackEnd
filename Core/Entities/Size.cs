using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Size
    {
        public int Id { get; set; }

        public string NameSize { get; set; } = null!;

        public virtual ICollection<Epp> Epps { get; set; } = new List<Epp>();
    }
}