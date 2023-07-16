using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;
using VentionTestTask.Application.IServices;
using VentionTestTask.Application.Loggings;
using VentionTestTask.Application.Validations.Categories;
using VentionTestTask.Domain.DTOs.Categories;
using VentionTestTask.Domain.DTOs.Products;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Domain.Exceptions;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Services.Categories
{
    public partial class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ILogging logging;
        private readonly ValidateCreateCategoriesDto validationCreate;
        private readonly ValidateUpdateCategoriesDto validationUpdate;

        public CategoryService(ICategoryRepository categoryRepository, 
            ILogging logging, ValidateCreateCategoriesDto validationCreate, 
            ValidateUpdateCategoriesDto validationUpdate)
        {
            this.categoryRepository = categoryRepository;
            this.logging = logging;
            this.validationCreate = validationCreate;
            this.validationUpdate = validationUpdate;
        }

        public async Task<Category> AddCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            try
            {
                if (createCategoryDto is null)
                {
                    throw new ArgumentNullException("CategoryDto is null");
                }

                ValidationResult validationResult = this.validationCreate.Validate(createCategoryDto);
                Validate(validationResult);

                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = createCategoryDto.Name,
                };

                return await this.categoryRepository.AddAsync(category);
            }
            catch (ArgumentNullException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed CategoryDto validation error occured. Try again!", exception);
            }
            catch (InvalidDtoException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed CategoryDto validation error occured. Try again!", exception);
            }
            catch (SqlException exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedStorageExceptions("Failed category storage error occured. Contact support!", exception);
            }
            catch (Exception exception)
            {
                this.logging.LogCritical(exception);

                throw new FailedServiceExceptions("Unexpected system error occured. Contact support!", exception);
            }
        }

        public Task DeleteCategoryAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> RetrieveAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> RetrieveCategoryByIdAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
