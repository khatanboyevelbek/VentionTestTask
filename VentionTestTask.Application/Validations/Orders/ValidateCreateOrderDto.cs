using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VentionTestTask.Domain.DTOs.Orders;

namespace VentionTestTask.Application.Validations.Orders
{
    public class ValidateCreateOrderDto : AbstractValidator<CreateOrderDto>
    {
        public ValidateCreateOrderDto()
        {
            RuleFor(s => s.OrderDate).NotNull().NotEmpty()
                .WithMessage("Please provide a valid date");

            RuleFor(s => s.UserId).NotNull().NotEmpty()
               .WithMessage("Please provide a valid userId");

            RuleFor(s => s.ProductId).NotNull()
                .WithMessage("Password should be minumum 8 characters");

            RuleFor(s => s.TotalAmount).NotNull().NotEmpty()
                .WithMessage("Please provide valid total amount");
        }
    }
}
