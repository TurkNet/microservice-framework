using System.Threading;
using System.Threading.Tasks;

namespace Noctools.TnMon.Api.Infrastructure.Adapters
{
    public class MailAdapter : IMailAdapter
    {
        public Task SendAsync(string @from, string to, string body, string subject, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
