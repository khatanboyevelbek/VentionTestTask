﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
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
        private readonly IValidator<CreateCategoryDto> validationCreate;
        private readonly IValidator<UpdateCategoryDto> validationUpdate;

        public CategoryService(ICategoryRepository categoryRepository, 
            ILogging logging, IValidator<CreateCategoryDto> validationCreate,
            IValidator<UpdateCategoryDto> validationUpdate)
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

                ValidationResult validationResult = await this.validationCreate.ValidateAsync(createCategoryDto);
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

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            try
            {
                if (categoryId == Guid.Empty)
                {
                    throw new ArgumentException("ProductId cannot be null");
                }

                Category existingCategory = await this.categoryRepository.SelectById(categoryId);

                if (existingCategory == null)
                {
                    throw new NotFoundExceptions("Category is not found with this Id");
                }

                await this.categoryRepository.DeleteAsync(existingCategory);
            }
            catch (ArgumentNullException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed CategoryDto validation error occured. Try again!", exception);
            }
            catch (NotFoundExceptions exception)
            {
                this.logging.LogError(exception);

                throw new ItemDependencyExceptions("Category is not found. Try again!", exception);
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

        public IQueryable<Category> RetrieveAllCategoriesAsync()
        {
            try
            {
                return this.categoryRepository.SelectAll();
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

        public async Task<Category> RetrieveCategoryByIdAsync(Guid categoryId)
        {
            try
            {
                if (categoryId == Guid.Empty)
                {
                    throw new ArgumentException("CategoryId cannot be null");
                }

                Category existingCategory = await this.categoryRepository.SelectById(categoryId);

                if (existingCategory == null)
                {
                    throw new NotFoundExceptions("Category is not found with this Id");
                }

                return existingCategory;
            }
            catch (ArgumentNullException exception)
            {
                this.logging.LogError(exception);

                throw new DtoValidationExceptions("Failed CategoryDto validation error occured. Try again!", exception);
            }
            catch (NotFoundExceptions exception)
            {
                this.logging.LogError(exception);

                throw new ItemDependencyExceptions("Category is not found. Try again!", exception);
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

        public async Task<Category> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                if (updateCategoryDto is null)
                {
                    throw new ArgumentNullException("ProductDto is null");
                }

                ValidationResult validationResult = await this.validationUpdate.ValidateAsync(updateCategoryDto);
                Validate(validationResult);

                Category existingCategory = await this.categoryRepository.SelectById(updateCategoryDto.Id);

                existingCategory.Name = updateCategoryDto.Name;

                return await this.categoryRepository.UpdateAsync(existingCategory);
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
    }
}
