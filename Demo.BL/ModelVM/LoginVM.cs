using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.ModelVM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(50)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
