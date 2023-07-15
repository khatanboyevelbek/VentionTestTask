using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VentionTestTask.Domain.Entities;

namespace VentionTestTask.Infrastructure.Data
{
    public static class SeedInitialData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Electronics"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Clothing"
                });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("df57efe5-ea5a-4f92-b8fe-fcfc6af28ba8"),
                    Name = "Phone",
                    Description = "Smartphone",
                    Price = 599.99m,
                    Quantity = 10
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("76f8a154-bb00-4e39-91df-a8571323bf55"),
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Password = "password123",
                    Address = "123 Main St",
                    Phone = "555-1234",
                    CreatedDate = DateTimeOffset.UtcNow,
                    UpdatedDate = DateTimeOffset.UtcNow
                });

            modelBuilder.Entity<Order>().HasData(
               new Order
               {
                   Id = Guid.NewGuid(),
                   OrderDate = DateTime.Now,
                   TotalAmount = 99.99m,
                   UserId = Guid.Parse("76f8a154-bb00-4e39-91df-a8571323bf55"),
                   ProductId = Guid.Parse("df57efe5-ea5a-4f92-b8fe-fcfc6af28ba8")
               });
        }
    }
}
