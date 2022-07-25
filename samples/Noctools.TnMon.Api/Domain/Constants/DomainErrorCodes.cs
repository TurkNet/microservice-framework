using System.Diagnostics.CodeAnalysis;

namespace Noctools.TnMon.Api.Domain.Constants
{
    [ExcludeFromCodeCoverage]
    public static class DomainErrorCodes
    {
        public static readonly string EDAnakin1001 = "LogDoesNotExists";
        public static readonly string EDAnakin1002 = "ReferenceLogIndexIdCanNotBeBull";
        public static readonly string EDAnakin1003 = "UndefinedEventCanNotOccured";
        public static readonly string EDAnakin1004 = "HostNameCanNotBeNull";
        public static readonly string EDAnakin1005 = "MessageCanNotBeNull";
        public static readonly string EDAnakin1006 = "LogDateCanNotBeNull";
        public static readonly string EDAnakin1007 = "PopNameCanNotBeNull";
        public static readonly string EDAnakin1008 = "CategoryCanNotBeNull";
        public static readonly string EDAnakin1009 = "DiscoveryDateCanNotBeNull";
        public static readonly string EDAnakin1010 = "NocUpdateLineItemDescriptionCanNotBeBull";
        public static readonly string EDAnakin1011 = "NocInformationCouldNotBeCreate";
        public static readonly string EDAnakin1012 = "Couldn't found the log# {0}.";
        public static readonly string EDAnakin1013 = "DeviceNameCanNotBeNull";
        public static readonly string EDAnakin1014 = "ProductIdCanNotBeZero";
        public static readonly string EDAnakin1015 = "TicketIdCanNotBeZero";
        public static readonly string EDAnakin1016 = "Couldn't found the noc# {0}.";
    }
}
