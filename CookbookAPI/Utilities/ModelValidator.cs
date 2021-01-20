using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookbookAPI.Utilities
{
    public static class ModelValidator
    {
        public static void Validate(object model)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, new ValidationContext(model), validationResults))
            {
                throw new ValidationException(nameof(model));
            }
        }
    }
}
