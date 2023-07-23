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
    public class ValidateUpdateOrderDto : AbstractValidator<UpdateOrderDto>
    {
        public ValidateUpdateOrderDto(IOrderRepository orderRepository) 
        {
            RuleFor(s => s.Id)
                .NotEmpty()
                .MustAsync(async (request, id, cancellationToken) =>
                    await orderRepository.SelectAll().AnyAsync(o => o.Id == request.Id, cancellationToken))
                .WithMessage("Order with this orderId is not found in the system"); 

            RuleFor(s => s.TotalAmount)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
