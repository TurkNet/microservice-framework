using System.Threading;
using System.Threading.Tasks;
using Nest;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Infrastructure
{
    public class LogCommandRepository : ILogCommandRepository
    {
        public Task<Log> UpdateAsync(Log log, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
