using Microsoft.EntityFrameworkCore;

namespace Noctools.EntityFramework.Db
{
    public interface IExtendDbContextOptionsBuilder
    {
        DbContextOptionsBuilder Extend(DbContextOptionsBuilder optionsBuilder,
            IDbConnStringFactory connectionStringFactory, string assemblyName);
    }
}
