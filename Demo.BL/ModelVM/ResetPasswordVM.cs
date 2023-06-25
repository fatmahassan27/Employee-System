using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.ModelVM
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(50)]
        public string Password { get; set; }
        [Compare("Password")]
        [MaxLength(50)]
        public string ConfirmPassword { get; set; }
        public string? Email { get; set; }

        public string? Token { get; set; }

    }
}
