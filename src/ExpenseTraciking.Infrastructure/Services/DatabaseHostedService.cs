using ExpenseTraciking.Domain.Entities;
using ExpenseTraciking.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTraciking.Infrastructure.Services
{
    public class DatabaseHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DatabaseHostedService> _logger;

        public DatabaseHostedService(IServiceProvider serviceProvider, ILogger<DatabaseHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DatabaseHostedService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("DatabaseHostedService is doing background work.");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    
                    var newExpense = new Expense
                    {
                        Description = "Hosted Service Expense",
                        Amount = 100,
                        Date = DateTime.Now
                    };

                    dbContext.Expenses.Add(newExpense);

                    
                    var newUser = new User
                    {
                        Username = "HostedServiceUser",
                        Email = "hostedserviceuser@example.com",
                        Password = "password" 
                    };

                    dbContext.Users.Add(newUser);

                    await dbContext.SaveChangesAsync(stoppingToken);
                }

                _logger.LogInformation("DatabaseHostedService has added a new expense and a new user.");

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); 
            }

            _logger.LogInformation("DatabaseHostedService is stopping.");
        }

    }
}
