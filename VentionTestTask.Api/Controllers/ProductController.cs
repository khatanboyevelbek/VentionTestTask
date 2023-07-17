using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VentionTestTask.Application.IServices;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Domain.DTOs.Products;
using VentionTestTask.Domain.Exceptions;

namespace VentionTestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : RESTFulController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public ActionResult GetAllProducts()
        {
            try
            {
                var result = this.productService.RetrieveAllProductsAsync();

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
        public async Task<ActionResult> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.productService.DeleteProductAsync(id);

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
        public async Task<ActionResult> PostProductAsync([FromBody] CreateProductDto createProductDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.productService.AddProductAsync(createProductDto);

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
        public async Task<ActionResult> GetProductAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.productService.RetrieveProductByIdAsync(id);

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
        public async Task<ActionResult> PutProductAsync([FromBody] UpdateProductDto updateProductDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.productService.UpdateProductAsync(updateProductDto);

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
