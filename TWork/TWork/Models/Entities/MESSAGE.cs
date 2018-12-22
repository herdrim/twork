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

        [ForeignKey("MESSAGE_TYPE")]
        public int? MESSAGE_TYPE_ID { get; set; }
        public virtual MESSAGE_TYPE MESSAGE_TYPE { get; set; }

        [ForeignKey("TEAM")]
        public int? TEAM_ID { get; set; }
        public virtual TEAM TEAM { get; set; }

        [ForeignKey("COMMENT")]
        public int? COMMENT_ID { get; set; }
        public virtual COMMENT COMMENT { get; set; }
    }
}
