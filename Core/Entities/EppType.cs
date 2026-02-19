using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class EppType
    {
        public int Id { get; set; }

        public string NameType { get; set; } = null!;

        public virtual ICollection<EppDetail> EppDetails { get; set; } = new List<EppDetail>();
    }
}