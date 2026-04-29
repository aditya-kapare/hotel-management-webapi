using Repositories;

namespace Repositories.DataSeeder
{
    public interface ISeeder
    {
        Task SeedAsync(RepositoryContext db, CancellationToken cancellationToken = default);
    }
}