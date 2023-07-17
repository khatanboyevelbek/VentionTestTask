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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<User> dbSet;

        public UserRepository(AppDbContext context)
            : base(context)
        { 
            this.context = context;
            this.dbSet = this.context.Set<User>();
        }

        public IQueryable<User> SelectAll(Expression<Func<User, bool>> filter = null) =>
            filter is null ? this.dbSet.Include(u => u.Orders) : 
                this.dbSet.Where(filter).Include(u => u.Orders);
    }
}
