using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Validations.Orders
{
    public class ValidateCreateOrderDto : AbstractValidator<CreateOrderDto>
    {
        public ValidateCreateOrderDto(IUserRepository userRepository, 
            IProductRepository productRepository)
        {
            RuleFor(s => s.OrderDate)
                .NotEmpty()
                .LessThan(DateTime.UtcNow);

            RuleFor(s => s.UserId)
               .NotEmpty()
               .MustAsync(async (request, id, cancellationToken) =>
                    await userRepository.SelectAll().AnyAsync(u => u.Id == request.UserId, cancellationToken))
               .WithMessage("User with this id is not found in the system");

            RuleFor(s => s.ProductId)
                .NotNull()
                .MustAsync(async (request, id, cancellationToken) =>
                    await productRepository.SelectAll().AnyAsync(p => p.Id == request.ProductId))
                .WithMessage("Product with this productId is not found in the system");

            RuleFor(s => s.TotalAmount)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
