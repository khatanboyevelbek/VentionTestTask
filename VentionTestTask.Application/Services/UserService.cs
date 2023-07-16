using System.Linq.Expressions;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;
using VentionTestTask.Application.IServices;
using VentionTestTask.Application.Loggings;
using VentionTestTask.Application.Security;
using VentionTestTask.Application.Validations.Users;
using VentionTestTask.Domain.DTOs;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Domain.Exceptions;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Services
{
    public partial class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogging logging;
        private readonly ValidateCreateUserDto createValidation;
        private readonly ISecurityPassword securityPassword;


        public UserService(IUserRepository userRepository, ILogging logging,
            ValidateCreateUserDto createValidation,
            ISecurityPassword securityPassword)
        {
            this.userRepository = userRepository;
            this.logging = logging;
            this.createValidation = createValidation;
            this.securityPassword = securityPassword;
        }

        public async Task<User> AddUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                if (createUserDto is null)
                {
                    throw new NullDtoExceptions("UserDto is null");
                }

                ValidationResult validationResult = this.createValidation.Validate(createUserDto);
                Validate(validationResult);

                bool isUserExist = this.userRepository.SelectAll().Any(u => u.Email == createUserDto.Email);

                if (isUserExist)
                {
                    throw new AlreadyExistExceptions("User with this email is already exist");
                }

                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = createUserDto.Name,
                    Email = createUserDto.Email,
                    Password = this.securityPassword.Encrypt(createUserDto.Password),
                    Address = createUserDto.Address,
                    Phone = createUserDto.Phone,
                    CreatedDate = DateTimeOffset.Now,
                    UpdatedDate = DateTimeOffset.Now
                };

                var entity = await this.userRepository.AddAsync(user);

                return entity;
            }
            catch (NullDtoExceptions exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed UserDto validation error occured", exception);
            }
            catch (InvalidDtoException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed UserDto validation error occured", exception);
            }
            catch (AlreadyExistExceptions  exception)
            {
                this.logging.LogCritical(exception);

                throw new DtoValidationExceptions("User dependency validation error occured", exception);
            }
            catch (SqlException exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedStorageExceptions("Failed user storage error occured. Contact support!", exception);
            }
            catch (Exception exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedServiceExceptions("Unexpected system error occured. Contact support!", exception);
            }
        }

        public Task DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<User>> RetrieveAllUsersAsync(Expression<Func<User, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<User> RetrieveUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
