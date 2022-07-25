using System.Threading;
using System.Threading.Tasks;

namespace Noctools.TnMon.Api.Domain
{
    public interface ILogCommandRepository
    {
        Task<Log> UpdateAsync(Log log, CancellationToken cancellationToken);
    }
}
