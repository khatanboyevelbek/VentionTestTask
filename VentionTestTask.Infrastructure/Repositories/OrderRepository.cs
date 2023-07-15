using VentionTestTask.Domain.Entities;
using VentionTestTask.Infrastructure.Data;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context)
            : base(context)
        { }
    }
}
