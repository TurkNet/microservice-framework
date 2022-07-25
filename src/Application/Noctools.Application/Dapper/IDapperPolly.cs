using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Noctools.Application.Dapper
{
    public interface IDapperPolly
    {
        Task<IEnumerable<T>> QueryAsyncWithRetry<T>(IDbConnection cnn, string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null);

        Task<T> ExecuteReaderAsync<T>(IDbConnection cnn, string sql, object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<dynamic>> QueryAsync(IDbConnection cnn, string sql, object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

    }
}
