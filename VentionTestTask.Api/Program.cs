using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentionTestTask.Application.Loggings;
using VentionTestTask.Application.Security;
using VentionTestTask.Application.Validations.Users;
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
            builder.Services.AddScoped<ILogging, Logging>();
            builder.Services.AddTransient<ISecurityPassword, SecurityPassword>();
            builder.Services.AddTransient<ValidateCreateUserDto>();
            builder.Services.AddTransient<ValidateUpdateUserDto>();

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
    }
}