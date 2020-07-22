using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class Account
    {
        [Required(ErrorMessage = "Nhập User Name")]
        [DisplayName("User Name")]
        public String UserName { get; set; }
        
        public String Password { get; set; }
        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Nhập Full Name")]
        public String FullName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày sinh")]
        [Required(ErrorMessage = "Chọn ngày sinh")]
        public DateTime DOB { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Nhập email")]
        public String Email { get; set; }
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Nhập SĐT")]
        [Phone]
        public String PhoneNumber { get; set; }
        [DisplayName("Active")]

        public bool Active { get; set; }

        public  List<Account_Role> Account_Roles { get; set; }

    }
}
