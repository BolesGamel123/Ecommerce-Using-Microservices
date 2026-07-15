using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Database Migration Started.");

                    ApplyMigration(config);

                    logger.LogInformation("Database Migration Completed.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }

            }
            return host;
        }


        private static void ApplyMigration(IConfiguration configuration)
        {
            using var connection = new NpgsqlConnection(
                configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            connection.Open();

            connection.Execute(@"
               DROP TABLE IF EXISTS Coupon;

               CREATE TABLE Coupon(
               Id SERIAL PRIMARY KEY,
               ProductName VARCHAR(500) NOT NULL,
               Description TEXT,
               Amount INT
               );

            INSERT INTO Coupon(ProductName, Description, Amount)
             VALUES('Nike Air Zoom Pegasus','Comfortable running shoes with responsive cushioning.',60);

            INSERT INTO Coupon(ProductName, Description, Amount)
             VALUES('Adidas Ultraboost 21','High performance running shoes with boost technology.',70);
             ");
        }
    }
}
