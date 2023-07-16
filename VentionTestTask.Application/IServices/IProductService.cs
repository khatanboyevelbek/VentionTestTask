using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Domain.DTOs.Products;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Application.IServices
{
    public interface IProductService
    {
        public Task<Product> AddProductAsync(CreateProductDto createProductDto);
        public Task<Product> UpdateProductAsync(UpdateProductDto updateProductDto);
        public Task DeleteProductAsync(Guid productId);
        public IQueryable<Product> RetrieveAllProductsAsync();
        public Task<Product> RetrieveProductByIdAsync(Guid productId);
    }
}
