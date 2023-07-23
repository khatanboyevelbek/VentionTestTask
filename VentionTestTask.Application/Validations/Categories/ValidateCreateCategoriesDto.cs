using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using VentionTestTask.Domain.DTOs.Categories;

namespace VentionTestTask.Application.Validations.Categories
{
    public class ValidateCreateCategoriesDto : AbstractValidator<CreateCategoryDto>
    {
        public ValidateCreateCategoriesDto() 
        {
            RuleFor(s => s.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
