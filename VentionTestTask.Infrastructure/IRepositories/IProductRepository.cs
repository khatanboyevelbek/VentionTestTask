using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Infrastructure.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    { }
}
