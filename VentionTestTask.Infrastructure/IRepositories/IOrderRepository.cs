using System.Linq.Expressions;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Infrastructure.IRepositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public IQueryable<Order> SelectAll(Expression<Func<Order, bool>> filter = null);
    }
}
