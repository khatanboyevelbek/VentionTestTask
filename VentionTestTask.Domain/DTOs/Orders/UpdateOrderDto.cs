using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentionTestTask.Domain.DTOs.Orders
{
    public class UpdateOrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
