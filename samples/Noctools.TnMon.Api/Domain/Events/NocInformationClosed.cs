using MediatR;
using Noctools.Domain;


namespace Noctools.TnMon.Api.Domain
{
    public class NocInformationClosed : EventBase, INotification
    {
        public NocInformationClosed()
        {
        }
    }
}