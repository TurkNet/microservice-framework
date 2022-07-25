using Noctools.Domain;
using Noctools.TnMon.Api.Domain.Constants;
using Noctools.Utils;

namespace Noctools.TnMon.Api.Domain
{
    /// <summary>
    /// todo: ilerde elkda nested document (child) olarak da tutulabilir.
    /// her noc update için audit niteliğinde kayıtları yine document içinde child olarak tutabiliriz.
    /// https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/parent-child-relationships.html
    /// </summary>
    public class NocLineItem : EntityBase
    {
        public string Description { get; private set; }

        public NocLineItem(string description)
        {
            Description = description;
        }

        public static NocLineItem Load(string description)
        {
            Guard.That<DomainException>(string.IsNullOrEmpty(description),
                nameof(DomainErrorCodes.EDAnakin1001),
                DomainErrorCodes.EDAnakin1001);

            return new NocLineItem(description);
        }
    }
}