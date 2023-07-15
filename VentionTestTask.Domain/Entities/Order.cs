using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VentionTestTask.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}
