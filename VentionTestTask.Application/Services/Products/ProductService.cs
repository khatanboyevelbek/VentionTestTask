using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;
using VentionTestTask.Application.IServices;
using VentionTestTask.Application.Loggings;
using VentionTestTask.Application.Validations.Products;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Domain.DTOs.Products;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Domain.Exceptions;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Services.Products
{
    public partial class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ILogging logging;
        private readonly ValidateCreateProductDto validateCreate;

        public ProductService(IProductRepository productRepository, 
            ILogging logging, ValidateCreateProductDto validateCreate)
        {
            this.productRepository = productRepository;
            this.logging = logging;
            this.validateCreate = validateCreate;
        }

        public async Task<Product> AddProductAsync(CreateProductDto createProductDto)
        {
            try
            {
                if (createProductDto is null)
                {
                    throw new ArgumentNullException("UserDto is null");
                }

                ValidationResult validationResult = this.validateCreate.Validate(createProductDto);
                Validate(validationResult);

                var product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = createProductDto.Name,
                    Description = createProductDto.Description,
                    Price = createProductDto.Price,
                    Quantity = createProductDto.Quantity,
                };

                return await this.productRepository.AddAsync(product);
            }
            catch (ArgumentNullException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed ProductDto validation error occured. Try again!", exception);
            }
            catch (InvalidDtoException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed ProductDto validation error occured. Try again!", exception);
            }
            catch (SqlException exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedStorageExceptions("Failed product storage error occured. Contact support!", exception);
            }
            catch (Exception exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedServiceExceptions("Unexpected system error occured. Contact support!", exception);
            }
        }

        public async Task DeleteOrderAsync(Guid productId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("ProductId cannot be null");
                }

                Product existingProduct = await this.productRepository.SelectById(productId);

                if (existingProduct == null)
                {
                    throw new NotFoundExceptions("Product is not found with this Id");
                }

                await this.productRepository.DeleteAsync(existingProduct);
            }
            catch (ArgumentException exception)
            {
                this.logging.LogError(exception);

                throw new FailedArgumentExceptions("Failed argument error occured. Try again!", exception);
            }
            catch (NotFoundExceptions exception)
            {
                this.logging.LogError(exception);

                throw new ItemDependencyExceptions("Priduct is not found. Try again!", exception);
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

        public IQueryable<Product> RetrieveAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> RetrieveOrderByIdAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
