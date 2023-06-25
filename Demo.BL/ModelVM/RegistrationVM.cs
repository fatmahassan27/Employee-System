using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.BL.ModelVM
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = "Username is Required")]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(20)]
        public string Email { get; set; }
        [Required(ErrorMessage ="password Required")]
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }

    }
}
