using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.Entities
{
    public class MESSAGE_TYPE
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
    }
}
