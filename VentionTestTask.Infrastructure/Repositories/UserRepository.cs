using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTestTask.Domain.Entities;
using VentionTestTask.Infrastructure.Data;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context)
            : base(context)
        { }
    }
}
