using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using VentionTestTask.Domain.Exceptions;

namespace VentionTestTask.Application.Services.Categories
{
    public partial class CategoryService
    {
        private static void Validate(ValidationResult validationResult)
        {
            var invalidDtoException = new InvalidDtoException("CategoryDto is invalid");

            foreach (var error in validationResult.Errors)
            {
                invalidDtoException.UpsertDataList(error.PropertyName, error.ErrorMessage);
            }

            invalidDtoException.ThrowIfContainsErrors();
        }
    }
}
