using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TWork.Models.Entities
{
    public class TASK
    {
        [Key]
        public int ID { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime? DEATHLINE { get; set; }
        public DateTime? CREATE_TIME { get; set; }
        public DateTime? START_TIME { get; set; }
        public DateTime? END_TIME { get; set; }

        [ForeignKey("TASK_STATUS")]
        public int TASK_STATUS_ID { get; set; }
        public virtual TASK_STATUS TASK_STATUS { get; set; }

        [ForeignKey("TEAM")]
        public int TEAM_ID { get; set; }
        public virtual TEAM TEAM { get; set; }

        [ForeignKey("USER")]
        public string USER_ID { get; set; }
        public virtual USER USER { get; set; }

        public virtual ICollection<COMMENT> COMMENTs { get; set; }
    }
}
