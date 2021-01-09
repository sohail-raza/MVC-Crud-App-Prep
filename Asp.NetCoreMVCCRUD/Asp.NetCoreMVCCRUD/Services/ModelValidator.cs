using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Services
{
    public class ModelValidator : IModelValidator
    {
        public void Validate<T>(AbstractValidator<T> validator, T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ValidationException("Incomming model is null");
                }

                ValidationResult result = validator.Validate(model);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
            }
            catch (Exception ex)
            {
                throw new ValidationException(ex.Message);
            }
        }

        public void Validate<T>(AbstractValidator<T> validator, T model, Dictionary<string, object> parameters)
        {
            {
                try
                {
                    if (model == null)
                    {
                        throw new ValidationException("Incomming model is null");
                    }

                    if (parameters.Count == 0)
                    {
                        throw new ValidationException("No additional parameters passed into validator");
                    }

                    var context = new ValidationContext<T>(model);
                    foreach (var param in parameters)
                    {
                        context.RootContextData.Add(param);
                    }

                    ValidationResult result = validator.Validate(context);

                    if (!result.IsValid)
                    {
                        throw new ValidationException(result.Errors);
                    }
                }
                catch (Exception ex)
                {
                    throw new ValidationException(ex.Message);
                }
            }
        }
    }
}
