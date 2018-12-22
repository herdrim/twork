using System;
using System.Collections.Generic;
using System.Linq;

namespace TWork.Models.Entities
{
    public class TEAM
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        

        
        public virtual ICollection<USER> USERs { get; set; }
        public virtual ICollection<TASK> TASKs { get; set; }
    }
}
