using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Infrastructure.Data;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Category> dbSet;

        public CategoryRepository(AppDbContext context)
            : base(context)
        {
            this.context = context;
            this.dbSet = this.context.Set<Category>();
        }

        public IQueryable<Category> SelectAll(Expression<Func<Category, bool>> filter = null) =>
            filter is null ? this.dbSet.Include(u => u.Products) :
                this.dbSet.Where(filter).Include(u => u.Products);
    }
}
