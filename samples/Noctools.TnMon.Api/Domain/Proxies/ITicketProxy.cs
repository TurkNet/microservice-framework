using System.Threading.Tasks;

namespace Noctools.TnMon.Api.Domain
{
    public interface ITicketProxy
    {
        Task<int> CreateTicketAsync(int productId, string description);
    }
}
