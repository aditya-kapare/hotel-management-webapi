using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Repositories.DataSeeder
{
    public static class SeedRunner
    {
        public static async Task RunAsync(RepositoryContext db, CancellationToken cancellationToken = default)
        {
            await db.Database.MigrateAsync(cancellationToken);

            var seeders = new ISeeder[]
            {
                new RoomsSeeder(),
                new CustomersSeeder(),
                new CabDriversSeeder(),

                new StaysSeeder(),
                new DropPickRequestsSeeder()
            };

            foreach (var seeder in seeders)
            {
                Console.WriteLine($"Running seeder: {seeder.GetType().Name}");
                await seeder.SeedAsync(db, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
            }

            Console.WriteLine("Seeding finished.");
        }
    }
}
