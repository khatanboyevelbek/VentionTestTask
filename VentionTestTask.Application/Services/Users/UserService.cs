using FluentValidation;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;
using VentionTestTask.Application.IServices;
using VentionTestTask.Application.Loggings;
using VentionTestTask.Application.Security;
using VentionTestTask.Domain.DTOs.Users;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Domain.Exceptions;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Services
{
    public partial class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogging logging;
        private readonly IValidator<CreateUserDto> createValidation;
        private readonly IValidator<UpdateUserDto> updateValidation;
        private readonly ISecurityPassword securityPassword;


        public UserService(IUserRepository userRepository, ILogging logging,
            IValidator<CreateUserDto> createValidation,
            ISecurityPassword securityPassword,
            IValidator<UpdateUserDto> updateValidation)
        {
            this.userRepository = userRepository;
            this.logging = logging;
            this.createValidation = createValidation;
            this.securityPassword = securityPassword;
            this.updateValidation = updateValidation;
        }

        public async Task<User> AddUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                if (createUserDto is null)
                {
                    throw new ArgumentNullException("UserDto is null");
                }

                ValidationResult validationResult = await this.createValidation.ValidateAsync(createUserDto);
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
            catch (ArgumentNullException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed UserDto validation error occured. Try again!", exception);
            }
            catch (InvalidDtoException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed UserDto validation error occured. Try again!", exception);
            }
            catch (AlreadyExistExceptions  exception)
            {
                this.logging.LogCritical(exception);

                throw new ItemDependencyExceptions("User dependency validation error occured. Try again!", exception);
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

        public async Task DeleteUserAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    throw new ArgumentException("UserId cannot be null");
                }

                User existingUser = await this.userRepository.SelectById(userId);

                if (existingUser == null)
                {
                    throw new NotFoundExceptions("User is not found with this Id");
                }

                await this.userRepository.DeleteAsync(existingUser);
            }
            catch (ArgumentException exception)
            {
                this.logging.LogError(exception);

                throw new FailedArgumentExceptions("Failed argument error occured. Try again!", exception);
            }
            catch (NotFoundExceptions exception)
            {
                this.logging.LogError(exception);

                throw new ItemDependencyExceptions("User is not found. Try again!", exception);
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

        public IQueryable<User> RetrieveAllUsersAsync()
        {
            try
            {
                return this.userRepository.SelectAll();
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

        public async Task<User> RetrieveUserByIdAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    throw new ArgumentException("UserId cannot be null");
                }

                User retrievedUser =  await this.userRepository.SelectById(userId);

                if (retrievedUser == null)
                {
                    throw new NotFoundExceptions("User is not found with this Id");
                }

                return retrievedUser;
            }
            catch (ArgumentException exception)
            {
                this.logging.LogError(exception);

                throw new FailedArgumentExceptions("Failed argument error occured. Try again!", exception);
            }
            catch (NotFoundExceptions exception)
            {
                this.logging.LogError(exception);

                throw new ItemDependencyExceptions("User is not found. Try again!", exception);
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

        public async Task<User> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            try
            {
                if (updateUserDto is null)
                {
                    throw new ArgumentNullException("UserDto is null");
                }

                ValidationResult validationResult = await this.updateValidation.ValidateAsync(updateUserDto);
                Validate(validationResult);

                User retrievedUser = await this.userRepository.SelectById(updateUserDto.Id);

                retrievedUser.Name = updateUserDto.Name;
                retrievedUser.Email = updateUserDto.Email;
                retrievedUser.Password = this.securityPassword.Encrypt(updateUserDto.Password);
                retrievedUser.Address = updateUserDto.Address;
                retrievedUser.Phone = updateUserDto.Phone;
                retrievedUser.UpdatedDate = DateTimeOffset.Now;

                return await this.userRepository.UpdateAsync(retrievedUser);
            }
            catch (ArgumentNullException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed UserDto validation error occured. Try again!", exception);
            }
            catch (InvalidDtoException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed UserDto validation error occured. Try again!", exception);
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
    }
}
