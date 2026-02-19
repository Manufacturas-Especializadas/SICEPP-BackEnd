using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class EppDto
    {
        public string Name { get; set; } = null!;

        public string Area { get; set; } = null!;

        public string Position { get; set; } = null!;

        public string Shift { get; set; } = null!;

        public bool DeliveryEPPPrevious { get; set; }

        public int ReasonRequestId { get; set; }

        public int PreviousConditionId { get; set; }

        public List<EppDetailDto> Details { get; set; } = new();

    }
}