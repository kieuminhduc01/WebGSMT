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
        [Required(ErrorMessage = "Input User Name!")]
        [DisplayName("User Name")]
        public String UserName { get; set; }
        
        public String Password { get; set; }
        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Input your Full Name!")]
        public String FullName { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Of Birth")]
        [Required(ErrorMessage = "Choose your date of birth!")]
        public DateTime DOB { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Input your email!")]
        public String Email { get; set; }
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Input your Phone Number!")]
        [Phone]
        public String PhoneNumber { get; set; }
        [DisplayName("Active")]

        public string formatDate()
        {
            return this.DOB==null?DateTime.Now.ToString("dd/MM/yyyy"): this.DOB.ToString("dd/MM/yyyy");
        }
        public bool Active { get; set; }

        public  List<Account_Role> Account_Roles { get; set; }

    }
}
