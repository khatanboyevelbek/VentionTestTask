using FluentValidation;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;
using VentionTestTask.Application.IServices;
using VentionTestTask.Application.Loggings;
using VentionTestTask.Application.Validations.Orders;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Domain.Exceptions;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Services.Orders
{
    public partial class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ILogging logging;
        private readonly IValidator<CreateOrderDto> validateCreate;
        private readonly IValidator<UpdateOrderDto> validateUpdate;

        public OrderService(IOrderRepository orderRepository,
            ILogging logging,
            IValidator<CreateOrderDto> validateCreate,
            IValidator<UpdateOrderDto> validateUpdate)
        { 
            this.orderRepository = orderRepository;
            this.logging = logging;
            this.validateCreate = validateCreate;
            this.validateUpdate = validateUpdate;
        }

        public async Task<Order> AddOrderAsync(CreateOrderDto createOrderDto)
        {
            try
            {
                if (createOrderDto is null)
                {
                    throw new ArgumentNullException("UserDto is null");
                }

                ValidationResult validationResult = await this.validateCreate.ValidateAsync(createOrderDto);
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
            try
            {
                return this.orderRepository.SelectAll();
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

        public async Task<Order> RetrieveOrderByIdAsync(Guid orderId)
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

                return existingOrder; 
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

        public async Task<Order> UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            try
            {
                if (updateOrderDto is null)
                {
                    throw new ArgumentNullException("OrderDto is null");
                }

                ValidationResult validationResult = await this.validateUpdate.ValidateAsync(updateOrderDto);
                Validate(validationResult);

                Order existingOrder = await this.orderRepository.SelectById(updateOrderDto.Id);

                existingOrder.TotalAmount = updateOrderDto.TotalAmount;

                return await this.orderRepository.UpdateAsync(existingOrder);
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
    }
}
