using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Infrastructure.Data;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Product> dbSet;

        public ProductRepository(AppDbContext context)
            : base(context)
        {
            this.context = context;
            this.dbSet = this.context.Set<Product>();
        }

        public IQueryable<Product> SelectAll(Expression<Func<Product, bool>> filter = null) =>
            filter is null ? this.dbSet.Include(u => u.Orders).Include(u => u.Categories) :
                this.dbSet.Where(filter).Include(u => u.Orders).Include(u => u.Categories);
    }
}
