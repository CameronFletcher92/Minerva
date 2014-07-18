using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaApi.Models
{
    public class DowntimeEvent
    {
        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public long Id { get; set; }

        public long EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
