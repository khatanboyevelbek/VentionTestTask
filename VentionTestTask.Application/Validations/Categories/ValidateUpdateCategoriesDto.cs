using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VentionTestTask.Domain.DTOs.Categories;
using VentionTestTask.Domain.DTOs.Products;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Validations.Categories
{
    public class ValidateUpdateCategoriesDto : AbstractValidator<UpdateCategoryDto>
    {
        public ValidateUpdateCategoriesDto(ICategoryRepository categoryRepository) 
        {
            RuleFor(s => s.Id)
                .NotEmpty()
                .MustAsync(async (request, id, cancellationToken) =>
                    await categoryRepository.SelectAll().AnyAsync(c => c.Id == request.Id, cancellationToken))
                .WithMessage("Category with this id is not found in the system");

            RuleFor(s => s.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
