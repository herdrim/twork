using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.Entities
{
    public class MESSAGE
    {
        [Key]
        public int ID { get; set; }
        public string TEXT { get; set; }
        public bool IS_READED { get; set; }
        public DateTime SEND_DATE { get; set; }

        [ForeignKey("MESSAGE_TYPE")]
        public int? MESSAGE_TYPE_ID { get; set; }
        public virtual MESSAGE_TYPE MESSAGE_TYPE { get; set; }

        [ForeignKey("TEAM")]
        public int? TEAM_ID { get; set; }
        public virtual TEAM TEAM { get; set; }

        [ForeignKey("COMMENT")]
        public int? COMMENT_ID { get; set; }
        public virtual COMMENT COMMENT { get; set; }

        [ForeignKey("USER_FROM")]
        public string USER_FROM_ID { get; set; }
        public virtual USER USER_FROM { get; set; }

        [ForeignKey("USER_TO")]
        public string USER_TO_ID { get; set; }
        public virtual USER USER_TO { get; set; }
    }
}
