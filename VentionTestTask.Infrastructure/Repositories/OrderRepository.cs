using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Infrastructure.Data;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Order> dbSet;

        public OrderRepository(AppDbContext context)
            : base(context)
        {
            this.context = context;
            this.dbSet = this.context.Set<Order>();
        }

        public IQueryable<Order> SelectAll(Expression<Func<Order, bool>> filter = null) =>
            filter is null ? this.dbSet : this.dbSet.Where(filter);
    }
}
