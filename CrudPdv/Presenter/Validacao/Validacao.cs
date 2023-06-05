using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrudPdv.Presenter.Validacao
{
    public class ModelDataValidation
    {
        public void Validar(object model)
        {
            string errorMessage = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, results, true);
            if (!isValid)
            {
                foreach (var item in results)
                    errorMessage += "- " + item.ErrorMessage + "\n";
                throw new Exception(errorMessage);
            }

        }
    }
}
