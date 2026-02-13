using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Epp
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Area { get; set; } = null!;

        public string Position { get; set; } = null!;

        public string Shift { get; set; } = null!;

        public int RequestedQuantity { get; set; }

        public bool DeliveryEPPPrevious { get; set; }


        public int EppTypeId { get; set; }

        public int SizeId { get; set; }

        public int ReasonRequestId { get; set; }

        public int PreviousConditionId { get; set; }


        public virtual EppType EppType { get; set; } = null!;

        public virtual Size Size { get; set; } = null!;

        public virtual ReasonRequest ReasonRequest { get; set; } = null!;

        public virtual PreviousCondition PreviousCondition { get; set; } = null!;
    }
}