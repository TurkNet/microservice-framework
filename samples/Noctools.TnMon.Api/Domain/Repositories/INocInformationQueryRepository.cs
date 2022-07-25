using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Noctools.TnMon.Api.Domain
{
    public interface INocInformationQueryRepository
    {
        Task<IEnumerable<NocInformation>> GetOutageLogsAsync(CancellationToken cancellationToken);
    }
}
