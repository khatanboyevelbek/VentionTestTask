using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VentionTestTask.Infrastructure.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<T> SelectById(Guid id);
    }
}
