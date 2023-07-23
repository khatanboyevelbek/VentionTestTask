using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using VentionTestTask.Application.IServices;
using VentionTestTask.Application.Loggings;
using VentionTestTask.Application.Security;
using VentionTestTask.Application.Services;
using VentionTestTask.Application.Services.Categories;
using VentionTestTask.Application.Services.Orders;
using VentionTestTask.Application.Services.Products;
using VentionTestTask.Application.Validations.Categories;
using VentionTestTask.Application.Validations.Orders;
using VentionTestTask.Application.Validations.Products;
using VentionTestTask.Application.Validations.Users;
using VentionTestTask.Domain.DTOs.Categories;
using VentionTestTask.Domain.DTOs.Orders;
using VentionTestTask.Infrastructure.Data;
using VentionTestTask.Infrastructure.IRepositories;
using VentionTestTask.Infrastructure.Repositories;

namespace VentionTestTask.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            RegisterDbContext(builder.Services, builder.Configuration);
            RegisterRepositories(builder.Services);
            RegisterUtilities(builder.Services);
            RegisterServices(builder.Services);


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                try
                {
                    await next(context);
                }
                catch (Exception e)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    ProblemDetails problem = new()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = "Server error",
                        Title = "Server error",
                        Detail = "An internal server error occured"
                    };

                    string json = JsonSerializer.Serialize(problem);

                    await context.Response.WriteAsync(json);
                }
            });

            app.MapControllers();

            app.Run();
        }

        private static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
        }

        private static void RegisterUtilities(IServiceCollection services)
        {
            services.AddScoped<ILogging, Logging>();
            services.AddTransient<ISecurityPassword, SecurityPassword>();
            services.AddTransient<ValidateCreateUserDto>();
            services.AddTransient<ValidateUpdateUserDto>();
            services.AddTransient<IValidator<CreateOrderDto>, ValidateCreateOrderDto>();
            services.AddTransient<IValidator<UpdateOrderDto>, ValidateUpdateOrderDto >();
            services.AddTransient<ValidateCreateProductDto>();
            services.AddTransient<ValidateUpdateProductDto>();
            services.AddTransient<IValidator<CreateCategoryDto>, ValidateCreateCategoriesDto>();
            services.AddTransient<IValidator<UpdateCategoryDto>, ValidateUpdateCategoriesDto>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
        }
    }
}