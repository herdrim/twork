using System;
using System.Collections.Generic;
using System.Linq;

namespace TWork.Models.Entities
{
    public class TASK
    {
        public int ID { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }

        public virtual TEAM TEAM { get; set; }
        public virtual USER USER { get; set; }
        public virtual ICollection<COMMENT> COMMENTs { get; set; }
    }
}
