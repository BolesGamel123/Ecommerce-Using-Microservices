using Dapper;
using Discount.Core.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Core.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM Coupon WHERE ProductName =@productName",
                new
                {
                    ProductName = productName
                });
            if (coupon == null)
            {
                return new Coupon() { Amount = 0, Description = "No Discount Avaliable For this Product", ProductName = "No Discount" };
            }
            else
                return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(
            _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
        await connection.ExecuteAsync(
            @"INSERT INTO Coupon(ProductName, Description, Amount)
              VALUES (@ProductName, @Description, @Amount)",
            coupon);

            return affected > 0;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(
                                       _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync(
                       @"UPDATE Coupon
                       SET Description = @Description,
                       Amount = @Amount,
                       ProductName = @ProductName
                       WHERE Id = @Id",
                      coupon);

            return affected > 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(
                                       _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =await connection.ExecuteAsync(
                    @"DELETE FROM Coupon
                    WHERE ProductName = @ProductName",
                    new
                    {
                        ProductName = productName
                    });

            return affected > 0;
        }
    }
}
