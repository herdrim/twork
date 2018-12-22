using System;
using System.Collections.Generic;
using System.Linq;

namespace TWork.Models.Entities
{
    public class COMMENT
    {
        public int ID { get; set; }
        public string CONTENT { get; set; }

        public virtual USER USER { get; set; }
        public virtual TASK TASK { get; set; }
    }
}
