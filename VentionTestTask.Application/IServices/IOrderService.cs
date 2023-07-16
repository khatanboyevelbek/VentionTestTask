using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Domain.DTOs.Users;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Application.IServices
{
    public interface IOrderService
    {
        public Task<Order> AddOrderAsync(CreateOrderDto createOrderDto);
        public Task<Order> UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        public Task DeleteOrderAsync(Guid orderId);
        public IQueryable<Order> RetrieveAllOrdersAsync();
        public Task<Order> RetrieveOrderByIdAsync(Guid orderId);
    }
}
