using System.Threading;
using System.Threading.Tasks;

namespace Noctools.TnMon.Api.Infrastructure.Adapters
{
    public interface IMailAdapter
    {
        Task SendAsync(string from, string to, string body, string subject, CancellationToken cancellationToken);
    }
}
