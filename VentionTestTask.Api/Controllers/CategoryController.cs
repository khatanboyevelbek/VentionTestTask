using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VentionTestTask.Application.IServices;
using VentionTestTask.Domain.DTOs.Categories;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Domain.Exceptions;

namespace VentionTestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : RESTFulController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult GetAllOrders()
        {
            try
            {
                var result = this.categoryService.RetrieveAllCategoriesAsync();

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
        public async Task<ActionResult> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.categoryService.DeleteCategoryAsync(id);

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
        public async Task<ActionResult> PostCategoryAsync([FromBody] CreateCategoryDto createCategoryDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.categoryService.AddCategoryAsync(createCategoryDto);

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
        public async Task<ActionResult> GetCategoryAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.categoryService.RetrieveCategoryByIdAsync(id);

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
        public async Task<ActionResult> PutCategoryAsync([FromBody] UpdateCategoryDto updateCategoryDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.categoryService.UpdateCategoryAsync(updateCategoryDto);

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
