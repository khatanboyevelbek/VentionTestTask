using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VentionTestTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a122e00c-eb68-4328-bff0-3a121ecdd805"), "Clothing" },
                    { new Guid("d6ae60c1-2ffd-4160-acbd-359ef232c3b9"), "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Quantity" },
                values: new object[] { new Guid("df57efe5-ea5a-4f92-b8fe-fcfc6af28ba8"), "Smartphone", "Phone", 599.99m, 10 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedDate", "Email", "Name", "Password", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("76f8a154-bb00-4e39-91df-a8571323bf55"), "123 Main St", new DateTimeOffset(new DateTime(2023, 7, 15, 14, 10, 16, 431, DateTimeKind.Unspecified).AddTicks(9665), new TimeSpan(0, 0, 0, 0, 0)), "john.doe@example.com", "John Doe", "password123", "555-1234", new DateTimeOffset(new DateTime(2023, 7, 15, 14, 10, 16, 431, DateTimeKind.Unspecified).AddTicks(9666), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "ProductId", "TotalAmount", "UserId" },
                values: new object[] { new Guid("752b4f51-f03d-4d99-ad48-74e21baccc38"), new DateTime(2023, 7, 15, 19, 10, 16, 431, DateTimeKind.Local).AddTicks(9684), new Guid("df57efe5-ea5a-4f92-b8fe-fcfc6af28ba8"), 99.99m, new Guid("76f8a154-bb00-4e39-91df-a8571323bf55") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a122e00c-eb68-4328-bff0-3a121ecdd805"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d6ae60c1-2ffd-4160-acbd-359ef232c3b9"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("752b4f51-f03d-4d99-ad48-74e21baccc38"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("df57efe5-ea5a-4f92-b8fe-fcfc6af28ba8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76f8a154-bb00-4e39-91df-a8571323bf55"));
        }
    }
}
