using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Noctools.TnMon.Api.Infrastructure
{
    /// <summary>
    /// todo: must be use a base class under an adapter implementation
    /// https://dotnetcoretutorials.com/2020/07/11/dapper-with-mysql-postgresql-on-net-core/
    /// </summary>
    public class ConnectionStringBase
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Netone2004 => new SqlConnection(_configuration.GetConnectionString("Netone2004"));
    }
}