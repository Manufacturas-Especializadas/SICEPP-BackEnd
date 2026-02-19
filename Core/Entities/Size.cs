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

        public virtual ICollection<EppDetail> EppDetails { get; set; } = new List<EppDetail>();
    }
}