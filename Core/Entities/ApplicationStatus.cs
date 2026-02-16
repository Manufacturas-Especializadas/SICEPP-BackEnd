using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ApplicationStatus
    {
        public int Id { get; set; }

        public string nameStatus { get; set; } = null!;

        public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}