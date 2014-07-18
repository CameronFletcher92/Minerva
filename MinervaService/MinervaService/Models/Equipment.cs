using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaApi.Models
{
    public class Equipment
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public long? ParentId { get; set; }


        [ForeignKey("ParentId")]
        public Equipment Parent { get; set; }

        public List<Equipment> Children { get; set; }

        public List<DowntimeEvent> DowntimeEvents { get; set; }
    }
}
