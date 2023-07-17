using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Infrastructure.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public IQueryable<User> SelectAll(Expression<Func<User, bool>> filter = null);
    }
}
