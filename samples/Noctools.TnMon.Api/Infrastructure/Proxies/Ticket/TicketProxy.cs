using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Noctools.Application.Dapper;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Infrastructure
{
    public class TicketProxy : ConnectionStringBase, ITicketProxy
    {
        private readonly IDapperPolly _dapperPolly;


        public TicketProxy(IDapperPolly dapperPolly, IConfiguration configuration) : base(configuration)
        {
            _dapperPolly = dapperPolly;
        }

        
        /// <summary>
        /// https://martinfowler.com/eaaCatalog/transactionScript.html
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<int> CreateTicketAsync(int productId, string description)
        {
            using (Netone2004)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MUSTERI", CustomerType.Turknet);
                parameters.Add("@ISLEMGRUBU", ProblemGroup.Noc);
                parameters.Add("@KUL_SORUN", ProblemType.Telekom);
                parameters.Add("@HIZMET", productId);
                parameters.Add("@ACIKLAMA", string.IsNullOrEmpty(description) ? string.Empty : description);
                parameters.Add("@TICKETID", -1, DbType.Int32, ParameterDirection.InputOutput);
                parameters.Add("@ATANAN", string.Empty);

                var result = await _dapperPolly.QueryAsyncWithRetry<dynamic>(Netone2004,
                    "n1w_des_add_destek",
                    parameters,
                    null,
                    null,
                    CommandType.StoredProcedure);

                return parameters.Get<int>("@TICKETID");
            }
        }
    }
}