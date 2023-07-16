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

        public Task DeleteOrderAsync(Guid productId)
        {
            throw new NotImplementedException();
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
