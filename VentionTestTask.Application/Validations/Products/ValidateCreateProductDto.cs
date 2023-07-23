using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VentionTestTask.Domain.DTOs.Products;

namespace VentionTestTask.Application.Validations.Products
{
    public class ValidateCreateProductDto : AbstractValidator<CreateProductDto>
    {
        public ValidateCreateProductDto() 
        {
            RuleFor(s => s.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(s => s.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(s => s.Price)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(s => s.Quantity)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
