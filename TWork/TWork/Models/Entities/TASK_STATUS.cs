using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.Entities
{
    public class TASK_STATUS
    {
        public int ID { get; set; }
        public string NAME { get; set; }

        [ForeignKey("TEAM")]
        public int? TEAM_ID { get; set; }
        public virtual TEAM TEAM { get; set; }

        [ForeignKey("PREV_STATUS")]
        public int? PREV_STATUS_ID { get; set; }
        public virtual TASK_STATUS PREV_STATUS { get; set; }

        [ForeignKey("NEXT_STATUS")]
        public int? NEXT_STATUS_ID { get; set; }
        public virtual TASK_STATUS NEXT_STATUS { get; set; }
    }
}
