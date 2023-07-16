using FluentValidation.Results;
using VentionTestTask.Application.IServices;
using VentionTestTask.Application.Loggings;
using VentionTestTask.Application.Validations.Orders;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Domain.DTOs.Users;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Domain.Exceptions;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Services.Orders
{
    public partial class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ILogging logging;
        private readonly ValidateCreateOrderDto validateCreate;

        public OrderService(IOrderRepository orderRepository,
            ILogging logging,
            ValidateCreateOrderDto validateCreate) 
        { 
            this.orderRepository = orderRepository;
            this.logging = logging;
            this.validateCreate = validateCreate;
        }

        public async Task<Order> AddOrderAsync(CreateOrderDto createOrderDto)
        {
            try
            {
                if (createOrderDto is null)
                {
                    throw new ArgumentNullException("UserDto is null");
                }

                ValidationResult validationResult = this.validateCreate.Validate(createOrderDto);
                Validate(validationResult);

                var order = new Order()
                {
                    Id = Guid.NewGuid(),
                    TotalAmount = createOrderDto.TotalAmount,
                    OrderDate = createOrderDto.OrderDate,
                    UserId = createOrderDto.UserId,
                    ProductId = createOrderDto.ProductId,
                };

                return await this.orderRepository.AddAsync(order);
            }
            catch (ArgumentNullException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed OrderDto validation error occured. Try again!", exception);
            }
            catch (InvalidDtoException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed OrderDto validation error occured. Try again!", exception);
            }
        }

        public Task DeleteOrderAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> RetrieveAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> RetrieveOrderByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            throw new NotImplementedException();
        }
    }
}
