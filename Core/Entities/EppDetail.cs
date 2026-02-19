using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class EppDetail
    {
        public int Id { get; set; }

        public int EppId { get; set; }

        public int EppTypeId { get; set; }

        public int? SizeId { get; set; }

        public int RequestedQuantity { get; set; }

        public virtual Epp Epp { get; set; } = null!;

        public virtual EppType EppType { get; set; } = null!;

        public virtual Size? Size { get; set; }
    }
}