using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TWork.Models.Entities
{
    public class COMMENT
    {
        [Key]
        public int ID { get; set; }
        public string CONTENT { get; set; }

        [ForeignKey("ANSWER")]
        public int? ANSWER_ID { get; set; }
        public virtual COMMENT ANSWER { get; set; }

        [ForeignKey("PARENT_COMMENT")]
        public int? PARENT_COMMENT_ID { get; set; }
        public virtual COMMENT PARENT_COMMENT { get; set; }

        [ForeignKey("USER")]
        public string USER_ID { get; set; }
        public virtual USER USER { get; set; }

        [ForeignKey("TASK")]
        public int TASK_ID { get; set; }
        public virtual TASK TASK { get; set; }
    }
}
