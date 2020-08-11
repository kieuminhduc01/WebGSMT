using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.ModelView.Account
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage = "Nhập tài khoản!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu!")]
        public string Password { get; set; }
    }
}
