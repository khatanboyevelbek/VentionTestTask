using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTestTask.Domain.DTOs.Categories;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Application.IServices
{
    public interface ICategoryService
    {
        public Task<Category> AddCategoryAsync(CreateCategoryDto createCategoryDto);
        public Task<Category> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        public Task DeleteCategoryAsync(Guid categoryId);
        public IQueryable<Category> RetrieveAllCategoriesAsync();
        public Task<Category> RetrieveCategoryByIdAsync(Guid categoryId);
    }
}
