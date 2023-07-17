using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VentionTestTask.Infrastructure.Data;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await this.dbSet.AddAsync(entity);
            await this.context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task DeleteAsync(T entity)
        {
            EntityEntry<T> entityEntry = this.context.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task<T> SelectById(Guid id) =>
            await this.dbSet.FindAsync(id);

        public async Task<T> UpdateAsync(T entity)
        {
            var entityEntry = this.dbSet.Update(entity);
            await this.context.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
