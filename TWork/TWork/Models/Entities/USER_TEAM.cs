﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.Entities
{
    public class USER_TEAM
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("TEAM")]
        public int TEAM_ID { get; set; }
        public virtual TEAM TEAM { get; set; }

        [ForeignKey("USER")]
        public string USER_ID { get; set; }
        public virtual USER USER { get; set; }
    }
}
