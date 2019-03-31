using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TWork.Models.Entities
{
    public class TEAM
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }



        public virtual ICollection<USER_TEAM> USERS_TEAMs { get; set; }
        public virtual ICollection<TASK> TASKs { get; set; }
        public virtual ICollection<USER_TEAM_ROLES> USER_TEAM_ROLEs { get; set; }
        public virtual ICollection<ROLE> ROLEs { get; set; }
        public virtual ICollection<TASK_STATUS> TASK_STATUSes { get; set; }
    }
}
