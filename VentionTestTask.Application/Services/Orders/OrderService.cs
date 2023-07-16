using FluentValidation.Results;
using Microsoft.Data.SqlClient;
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
            catch (SqlException exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedStorageExceptions("Failed order storage error occured. Contact support!", exception);
            }
            catch (Exception exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedServiceExceptions("Unexpected system error occured. Contact support!", exception);
            }
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty)
                {
                    throw new ArgumentException("UserId cannot be null");
                }

                Order existingOrder = await this.orderRepository.SelectById(orderId);

                if (existingOrder == null)
                {
                    throw new NotFoundExceptions("Order is not found with this Id");
                }

                await this.orderRepository.DeleteAsync(existingOrder);
            }
            catch (ArgumentException exception)
            {
                this.logging.LogError(exception);

                throw new FailedArgumentExceptions("Failed argument error occured. Try again!", exception);
            }
            catch (NotFoundExceptions exception)
            {
                this.logging.LogError(exception);

                throw new ItemDependencyExceptions("Order is not found. Try again!", exception);
            }
            catch (SqlException exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedStorageExceptions("Failed order storage error occured. Contact support!", exception);
            }
            catch (Exception exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedServiceExceptions("Unexpected system error occured. Contact support!", exception);
            }
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
