using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataSeeder
{
    public class CabDriversSeeder : ISeeder
    {
        public async Task SeedAsync(RepositoryContext db, CancellationToken cancellationToken = default)
        {

            if (await db.CabDrivers.AnyAsync(cancellationToken))
                return;

            var drivers = new List<CabDriver>
            {
                new CabDriver
                {
                    GovernmentId = "MNQPK8368A",
                    Name = "Ramesh Jadhav",
                    Age = 35,
                    Gender = Gender.Male,
                    CarVendor = "Ola",
                    CarType = "Sedan"
                },
                new CabDriver
                {
                    GovernmentId = "34289120490",
                    Name = "Suresh Patil",
                    Age = 42,
                    Gender = Gender.Male,
                    CarVendor = "Uber",
                    CarType = "SUV"
                },
                new CabDriver
                {
                    GovernmentId = "DF349898DJ",
                    Name = "Amit Verma",
                    Age = 30,
                    Gender = Gender.Male,
                    CarVendor = "Private",
                    CarType = "Hatchback"
                },
                new CabDriver
                {
                    GovernmentId = "84958Sdfkjk",
                    Name = "Sunita Desai",
                    Age = 38,
                    Gender = Gender.Female,
                    CarVendor = "Ola",
                    CarType = "Sedan"
                },
                new CabDriver
                {
                    GovernmentId = "ASDFFG",
                    Name = "Karan Singh",
                    Age = 45,
                    Gender = Gender.Male,
                    CarVendor = "Uber",
                    CarType = "SUV"
                },
                new CabDriver
                {
                    GovernmentId = "NHKLI",
                    Name = "Neha Kulkarni",
                    Age = 33,
                    Gender = Gender.Female,
                    CarVendor = "Private",
                    CarType = "Sedan"
                }
            };

            await db.CabDrivers.AddRangeAsync(drivers, cancellationToken);
        }
    }
}