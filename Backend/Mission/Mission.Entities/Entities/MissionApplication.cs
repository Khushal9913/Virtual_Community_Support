using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Entities
{
    public class MissionApplication
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public int UserId { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool Status { get; set; } = false;

        [ForeignKey(nameof(MissionId))]
        public virtual Missions Missions { get; set; }
        public virtual User User { get; set; }
    }
}
