using ExpenseTraciking.Application.Interfaces;
using ExpenseTraciking.Infrastructure.Data;
using ExpenseTracking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTraciking.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Dependency Injection
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

        }
    }
}
