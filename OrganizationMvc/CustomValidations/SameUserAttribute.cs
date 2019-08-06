using DataAccesLayer.Repositorys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizationMvc.CustomValidations
{
    public class SameUserAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string val = value.ToString();
            UserRepository rep = new UserRepository();
            if (rep.Filter(val))
            {
                ErrorMessage = "Same username founded change username for registering";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}