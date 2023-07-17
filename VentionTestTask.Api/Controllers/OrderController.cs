using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VentionTestTask.Application.IServices;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Domain.DTOs.Users;
using VentionTestTask.Domain.Exceptions;

namespace VentionTestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : RESTFulController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public ActionResult GetAllOrders() 
        {
            try
            {
                var result = this.orderService.RetrieveAllOrdersAsync();

                return Ok(result);
            }
            catch (FailedStorageExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
            catch (FailedServiceExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default) 
        {
            try
            {
                await this.orderService.DeleteOrderAsync(id);

                return NoContent();
            }
            catch (FailedArgumentExceptions exception)
            {
                return BadRequest(exception.InnerException);
            }
            catch (ItemDependencyExceptions exception)
               when (exception.InnerException is NotFoundExceptions)
            {
                return NotFound(exception.InnerException);
            }
            catch (FailedStorageExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
            catch (FailedServiceExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostOrderAsync([FromBody] CreateOrderDto createOrderDto, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.orderService.AddOrderAsync(createOrderDto);

                return Created(result);
            }
            catch (DtoValidationExceptions exception)
            {
                return BadRequest(exception.InnerException);
            }
            catch (ItemDependencyExceptions exception)
            {
                return Conflict(exception.InnerException);
            }
            catch (FailedStorageExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
            catch (FailedServiceExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.orderService.RetrieveOrderByIdAsync(id);

                return Ok(result);
            }
            catch (FailedArgumentExceptions exception)
            {
                return BadRequest(exception.InnerException);
            }
            catch (ItemDependencyExceptions exception)
               when (exception.InnerException is NotFoundExceptions)
            {
                return NotFound(exception.InnerException);
            }
            catch (FailedStorageExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
            catch (FailedServiceExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutOrdersync([FromBody] UpdateOrderDto updateOrderDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.orderService.UpdateOrderAsync(updateOrderDto);

                return NoContent();
            }
            catch (FailedArgumentExceptions exception)
            {
                return BadRequest(exception.InnerException);
            }
            catch (ItemDependencyExceptions exception)
               when (exception.InnerException is NotFoundExceptions)
            {
                return NotFound(exception.InnerException);
            }
            catch (FailedStorageExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
            catch (FailedServiceExceptions exception)
            {
                return InternalServerError(exception.InnerException);
            }
        }
    }
}
