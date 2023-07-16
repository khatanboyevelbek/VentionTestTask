using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VentionTestTask.Domain.DTOs.Categories;
using VentionTestTask.Domain.DTOs.Products;

namespace VentionTestTask.Application.Validations.Categories
{
    public class ValidateUpdateCategoriesDto : AbstractValidator<UpdateCategoryDto>
    {
        public ValidateUpdateCategoriesDto() 
        {
            RuleFor(s => s.Id).NotNull().NotEmpty()
                .WithMessage("Please provide a valid Id");

            RuleFor(s => s.Name).NotNull().NotEmpty()
                .WithMessage("Please provide a valid name");
        }
    }
}
