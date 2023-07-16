using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VentionTestTask.Domain.DTOs.Products;

namespace VentionTestTask.Application.Validations.Products
{
    public class ValidateUpdateProductDto : AbstractValidator<UpdateProductDto>
    {
        public ValidateUpdateProductDto() 
        {
            RuleFor(s => s.Id).NotNull().NotEmpty()
               .WithMessage("Please provide a valid Id");

            RuleFor(s => s.Name).NotNull().NotEmpty()
                .WithMessage("Please provide a valid name");

            RuleFor(s => s.Description).NotNull().NotEmpty()
               .WithMessage("Please provide a valid description");

            RuleFor(s => s.Price).NotNull()
                .WithMessage("Please provide a valid price");

            RuleFor(s => s.Quantity).NotNull().NotEmpty()
                .WithMessage("Please provide valid quantity");
        }
    }
}
