using System.Threading.Tasks;

namespace Noctools.TnMon.Api.Domain
{
    public interface IProductProxy
    {
        Task<int> GetProductIdByHostNameAsync(string hostname);
    }
}
