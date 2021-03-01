using Microsoft.AspNetCore.Http;
using SafetyFund.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SafetyFund.Web.Models
{
    public class ProjectEditViewModel
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class ValidateImageAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var file = value as IFormFile;

                if (file == null)
                {
                    return ValidationResult.Success;
                }

                if (file.Length > (int)(0.3 * 1024 * 1024))
                {
                    return new ValidationResult("This image max size is 300 KB!");
                }

                var ext = Path.GetExtension(file.FileName);
                if (String.IsNullOrEmpty(ext) ||
                    (!String.Equals(ext, ".jpg") && !String.Equals(ext, ".jpeg") && !String.Equals(ext, ".png")))
                {
                    return new ValidationResult("This file format must be a JPG / PNG/ JPEG!");
                }

                return ValidationResult.Success;
            }
        }


        public Project Project { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Product Image")]
        [ValidateImage()]
        public IFormFile FeaturedImage { get; set; }
    }
}
