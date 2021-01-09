using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Services
{
    public interface IModelValidator
    {
        void Validate<T>(AbstractValidator<T> validator, T model);
        void Validate<T>(AbstractValidator<T> validator, T model, Dictionary<string, object> parameters);
    }
}
