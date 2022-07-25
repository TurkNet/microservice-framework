using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Noctools.Application.Dapper;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Infrastructure
{
    public class ProductProxy : ConnectionStringBase, IProductProxy
    {
        /// <summary>
        ///todo :  dapper ile like ve '' gibi hassas karakterler calısmıyor
        /// </summary>
        private const string GetProductIdQueryByHostName = @"SELECT SIRANO FROM HIZMET H
                  INNER JOIN TBLPOP P ON H.USERNAME = REPLACE(LOWER(P.POPNAME), 'ı', 'i')
                  INNER JOIN TC_DEVICE D ON P.POPID = D.POPID
                  WHERE D.HOSTNAME like @hostname
                  AND D.POPID > 0 AND D.IsActive = 1";

        private const string GetProductIdQueryByEquipIp = @"SELECT SIRANO FROM HIZMET H
                  INNER JOIN TBLPOP P ON H.USERNAME = REPLACE(LOWER(P.POPNAME), 'ı', 'i')
                  INNER JOIN TC_DEVICE D ON P.POPID = D.POPID AND p.POPID > 0
                  WHERE D.EQUIPIP = @hostname
                  AND D.POPID > 0 AND D.IsActive = 1";

        private readonly IDapperPolly _dapperPolly;

        public ProductProxy(IDapperPolly dapperPolly, IConfiguration configuration) : base(configuration)
        {
            _dapperPolly = dapperPolly;
        }

        public async Task<int> GetProductIdByHostNameAsync(string hostname)
        {
            string query;
            if (IsIp(hostname))
                query = GetProductIdQueryByEquipIp;
            else
            {
                hostname = EncodeForLike(hostname);
                if (HasTailBackbone(hostname))
                {
                    hostname = hostname.Replace(@".backbone.turk.net", "");
                }

                query = GetProductIdQueryByHostName;
            }

            using (Netone2004)
            {
                return await _dapperPolly.ExecuteReaderAsync<int>(Netone2004,
                    query, new {hostname = hostname});
            }
        }

        private static bool IsIp(string ip)
        {
            var regex = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}", RegexOptions.IgnoreCase);
            var matches = regex.Matches(ip);
            return matches.Count == 0 ? false : true;
        }

        private static bool HasTailBackbone(string hostName)
        {
            var regex = new Regex(@"\.backbone\.turk\.net");
            var matches = regex.Matches(hostName);
            return matches.Count == 0 ? false : true;
        }

        private static string EncodeForLike(string term)
        {
            term = term.Replace("[", "[[]").Replace("%", "[%]");
            term = "%" + term + "%";
            return term;
        }
    }
}