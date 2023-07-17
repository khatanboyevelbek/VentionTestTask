using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Infrastructure.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public IQueryable<Product> SelectAll(Expression<Func<Product, bool>> filter = null);
    }
}
