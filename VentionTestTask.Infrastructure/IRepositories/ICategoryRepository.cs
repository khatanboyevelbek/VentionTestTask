using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Infrastructure.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public IQueryable<Category> SelectAll(Expression<Func<Category, bool>> filter = null);
    }
}
