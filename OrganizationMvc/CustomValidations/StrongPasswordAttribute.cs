using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace OrganizationMvc.CustomValidations
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        public string Message { get; set; }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            string strval = value.ToString();
            bool intVarmi = false;
            bool upperVarmi = false;
            bool lowerVarmi = false;

            foreach (var item in strval)
            {
                if (Char.IsNumber(item))
                {
                    intVarmi = true;
                }
                else if(Char.IsUpper(item))
                {
                    upperVarmi = true;
                }
                else if(Char.IsLower(item))
                {
                    lowerVarmi = true;
                }
            }

            if (intVarmi && upperVarmi && lowerVarmi)
            {
                return true;
            }
            else
            {
                ErrorMessage = "parola en az bir büyük, bir küçük karakter ve en az bir rakam içermeli ";
                Message = ErrorMessage;
                return false;
            }
        }
    }
}