using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.Entities
{
    [Table("AspNetUsers")]
    public class USER : IdentityUser
    {
        public virtual ICollection<TEAM> TEAMs { get; set; }
        public virtual ICollection<TASK> TASKs { get; set; }
        public virtual ICollection<COMMENT> COMMENTs { get; set; }
    }
}
