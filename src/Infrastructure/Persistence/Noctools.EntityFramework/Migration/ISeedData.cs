using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Noctools.EntityFramework.Migration
{
    public interface ISeedData<in TDbContext>
        where TDbContext : DbContext
    {
        Task SeedAsync(TDbContext context);
    }

    public interface IAuthConfigSeedData<in TDbContext> : ISeedData<TDbContext>
        where TDbContext : DbContext
    {
    }
}
