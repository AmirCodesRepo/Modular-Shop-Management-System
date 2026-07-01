using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace _0_Framework.Application
{
    public class FileExtensionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtensions;
        public FileExtensionLimitationAttribute(string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            
            var fileExtensions = Path.GetExtension(file.FileName);

            if(!_validExtensions.Contains(fileExtensions)) return false;

            return true;
            
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-extensionfilelimitation", ErrorMessage);
            context.Attributes.Add("data-val-extensionfilelimitation-extensions", string.Join(",", _validExtensions));
        }
    }
}
