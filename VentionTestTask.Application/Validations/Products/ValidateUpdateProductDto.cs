using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VentionTestTask.Domain.DTOs.Products;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Validations.Products
{
    public class ValidateUpdateProductDto : AbstractValidator<UpdateProductDto>
    {
        public ValidateUpdateProductDto(IProductRepository productRepository) 
        {
            RuleFor(s => s.Id)
                .NotEmpty()
                .MustAsync(async (request, id, cancellationToken) =>
                    await productRepository.SelectAll().AnyAsync(p => p.Id == request.Id, cancellationToken))
                .WithMessage("Product with this productId is not found in the system");

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
