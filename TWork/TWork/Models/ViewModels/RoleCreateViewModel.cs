﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class RoleCreateViewModel
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        [Required]
        public bool CanCreateTask { get; set; }
        [Required]
        public bool CanAssignTask { get; set; }
        [Required]
        public bool CanComment { get; set; }
        [Required]
        public bool CanManageUsers { get; set; }
    }
}
