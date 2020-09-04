using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFDbFirstApproachExample.CustomValidations
{
    public class PhotoExtValid : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase fileBase = value as HttpPostedFileBase;
            if (fileBase == null)
            {
                return ValidationResult.Success;
            }
            else
            {

                string ext = fileBase.ContentType;

                if (ext == ".jpeg" || ext == ".jpg" || ext == ".png")
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(this.ErrorMessage);
                }
            }


        }
    }
}