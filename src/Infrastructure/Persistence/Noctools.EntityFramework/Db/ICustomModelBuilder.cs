using Microsoft.EntityFrameworkCore;

namespace Noctools.EntityFramework.Db
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
