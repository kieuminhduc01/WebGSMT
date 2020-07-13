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
        [DisplayName("User Name")]
        public String UserName { get; set; }

        public String Password { get; set; }
        [DisplayName("Họ và tên")]
        public String FullName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Ngày sinh")]
        public DateTime DOB { get; set; }
        [DisplayName("Email")]
        public String Email { get; set; }
        [DisplayName("Số điện thoại")]
        public String PhoneNumber { get; set; }
        [DisplayName("Trạng Thái")]
        public bool Active { get; set; }

        public List<Account_Role> Account_Roles { get; set; }

    }
}
