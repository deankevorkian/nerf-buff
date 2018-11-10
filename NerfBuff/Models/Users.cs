using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NerfBuff.Models
{
    public partial class Users
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage ="Username must contain only English letters and numbers,")]
        [StringLength(20)]
        [Display(Name = "User Name")]
        public string BlogUserName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Password")]
        public string BlogUserPassword { get; set; }

        public bool BlogIsAdmin { get; set; }
    }
}
