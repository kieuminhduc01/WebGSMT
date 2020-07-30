using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.ModelView.Account
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage = "Input User Name!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Input your Password!")]
        public string Password { get; set; }
    }
}
