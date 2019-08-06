using OrganizationMvc.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizationMvc.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Required]
        [SameUser]
        public string UserName { get; set; }
        [Required]
        [StrongPassword]
        public string Password { get; set; }

        public int UserType { get; set; }
    }
}