using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VentionTestTask.Application.IServices;
using VentionTestTask.Domain.DTOs.Users;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Domain.Exceptions;

namespace VentionTestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : RESTFulController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            try
            {
                IQueryable<User> result = this.userService.RetrieveAllUsersAsync();

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

        [HttpPost]
        public async Task<ActionResult> PostUserAsync([FromBody] CreateUserDto createUserDto, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                User result = await this.userService.AddUserAsync(createUserDto);

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.userService.DeleteUserAsync(id);

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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.userService.RetrieveUserByIdAsync(id);

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
        public async Task<ActionResult> PutUserAsync([FromBody] UpdateUserDto updateUserDto, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.userService.UpdateUserAsync(updateUserDto);

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
