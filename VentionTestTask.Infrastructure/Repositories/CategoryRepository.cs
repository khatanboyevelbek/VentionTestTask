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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context)
            : base(context)
        { }
    }
}
