using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Core.Entities
{
    public class Epp
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Area { get; set; } = null!;

        public string Position { get; set; } = null!;

        public string Shift { get; set; } = null!;

        public bool DeliveryEPPPrevious { get; set; }

        public int ReasonRequestId { get; set; }

        public int PreviousConditionId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<EppDetail> EppDetails { get; set; } = new List<EppDetail>();

        public virtual ReasonRequest ReasonRequest { get; set; } = null!;

        public virtual PreviousCondition PreviousCondition { get; set; } = null!;

        public Store? Store { get; set; }
    }
}
