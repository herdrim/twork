using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.Entities
{
    public class ROLE
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public bool IS_REQUIRED { get; set; }
        public bool IS_TEAM_OWNER { get; set; }
        public bool CAN_CREATE_TASK { get; set; }
        public bool CAN_ASSIGN_TASK { get; set; }
        public bool CAN_COMMENT { get; set; }
        public bool CAN_MANAGE_USERS { get; set; }


        public virtual ICollection<USER_TEAM_ROLES> USER_TEAM_ROLEs { get; set; }
    }
}
