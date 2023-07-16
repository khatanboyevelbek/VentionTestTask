using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VentionTestTask.Domain.DTOs.Orders;

namespace VentionTestTask.Application.Validations.Orders
{
    public class ValidateUpdateOrderDto : AbstractValidator<UpdateOrderDto>
    {
        public ValidateUpdateOrderDto() 
        {
            RuleFor(s => s.Id).NotNull()
                .WithMessage("Please provide valid OrderId");

            RuleFor(s => s.TotalAmount).NotNull().NotEmpty()
                .WithMessage("Please provide valid total amount");
        }
    }
}
