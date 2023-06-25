using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.ModelVM
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(20)]
        public string Email { get; set; }
    }
}
