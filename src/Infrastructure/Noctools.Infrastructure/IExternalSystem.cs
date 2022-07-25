using System.Threading.Tasks;

namespace Noctools.Infrastructure
{
    public interface IExternalSystem
    {
        Task<bool> Connect();
    }
}
