﻿using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models
{
    public class HiringManager
    {
        [Key]
        public int ManagerID { get; set; }
        //[Required]
        public string Name { get; set; }
        //[Required]
        public string Specializimi { get; set; }
        //[Required]
        public string Email { get; set; }
    }
}
