using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using VentionTestTask.Domain.Exceptions;

namespace VentionTestTask.Application.Services.Orders
{
    public partial class OrderService
    {
        private static void Validate(ValidationResult validationResult)
        {
            var invalidDtoException = new InvalidDtoException("OrderDto is invalid");

            foreach (var error in validationResult.Errors)
            {
                invalidDtoException.UpsertDataList(error.PropertyName, error.ErrorMessage);
            }

            invalidDtoException.ThrowIfContainsErrors();
        }
    }
}
