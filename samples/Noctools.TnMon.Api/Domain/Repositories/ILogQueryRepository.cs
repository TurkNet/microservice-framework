using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs;

namespace Noctools.TnMon.Api.Domain
{
    public interface ILogQueryRepository
    {
        Task<IEnumerable<Log>> GetLogsAsync(CancellationToken cancellationToken);
        Task<Log> FindOneAsync(string index, string id, CancellationToken cancellationToken);
    }
}