using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nest;
using Noctools.TnMon.Api.Controllers.UseCases;
using Noctools.TnMon.Api.Domain;
using Noctools.TnMon.Api.Domain.Constants;
using Noctools.Domain;
using Noctools.TnMon.Api.Contants;
using Noctools.Utils;

namespace Noctools.TnMon.Api.Domain
{
    /// <summary>
    /// https://medium.com/hackernoon/clean-domain-driven-design-in-10-minutes-6037a59c8b7b
    /// </summary>
    [ElasticsearchType(Name = ApplicationConstants.NocIndexType)]
    public class NocInformation : AggregateRootBase
    {
        [Text(Name = "category")] public string Category { get; set; }
        [Text(Name = "description")] public string Description { get; set; }
        [Text(Name = "discoveryDate")] public DateTime DiscoveryDate { get; set; }
        [Text(Name = "hostName")] public string HostName { get; set; }
        [Text(Name = "log_Date")] public DateTime LogDate { get; set; }
        [Text(Name = "message")] public string Message { get; set; }
        [Text(Name = "popName")] public string PopName { get; set; }
        [Text(Name = "status")] public string Status { get; set; }
        [Text(Name = "ticketNo")] public int TicketId { get; set; }
        [Text(Name = "recoveryTime")] public DateTime? RecoveryTime { get; set; }
        [Text(Name = "closingDescription")] public string ClosingDescription { get; set; }
        [Text(Name = "closingDate")] public DateTime? ClosingDate { get; set; }
        [Text(Ignore = true)] public IList<Device> DeviceList { get; set; }
        [Text(Name = "devices")] public string[] Devices { get; set; }

        // [Text(Name = "updateLineItems")] public IList<NocLineItem> LineItems { get; private set; }
        [Text(Name = "logIndexName")] public string LogIndexName { get; set; }
        [Text(Name = "logIndexId")] public string LogIndexId { get; set; }


        /// <summary>
        /// impedance mismatch for newtonsoft json
        /// </summary>
        public NocInformation()
        {
        }

        /// <summary>
        /// TODO : https://www.elastic.co/guide/en/elasticsearch/reference/current/nested.html ??
        /// business invariants
        /// </summary>
        /// <param name="logIndexName"></param>
        /// <param name="logIndexId"></param>
        /// <param name="hostName"></param>
        /// <param name="message"></param>
        /// <param name="logDate"></param>
        /// <param name="popName"></param>
        /// <param name="category"></param>
        /// <param name="discoveryDate"></param>
        /// <param name="description"></param>
        private NocInformation(string logIndexName, string logIndexId, string hostName, string message,
            DateTime logDate, string popName, string category, DateTime discoveryDate,
            string description)
        {
            LogIndexName = logIndexName;
            LogIndexId = logIndexId;
            HostName = hostName;
            Message = message;
            LogDate = logDate;
            PopName = popName;
            Category = category;
            DiscoveryDate = discoveryDate;
            Description = description;
        }

        public static async Task<NocInformation> LoadAsync(CreateNocInformationCommand nocInformationCommand,
            ITicketProxy ticketProxy, IProductProxy productProxy, CancellationToken cancellationToken)
        {
            Guard.That<DomainException>(string.IsNullOrEmpty(nocInformationCommand.LogIndexId),
                nameof(DomainErrorCodes.EDAnakin1002), DomainErrorCodes.EDAnakin1002);

            Guard.That<DomainException>(string.IsNullOrEmpty(nocInformationCommand.LogIndexName),
                nameof(DomainErrorCodes.EDAnakin1003), DomainErrorCodes.EDAnakin1003);

            Guard.That<DomainException>(string.IsNullOrEmpty(nocInformationCommand.HostName),
                nameof(DomainErrorCodes.EDAnakin1004), DomainErrorCodes.EDAnakin1004);

            Guard.That<DomainException>(string.IsNullOrEmpty(nocInformationCommand.Message),
                nameof(DomainErrorCodes.EDAnakin1005), DomainErrorCodes.EDAnakin1005);

            Guard.That<DomainException>(nocInformationCommand.LogDate == default(DateTime),
                nameof(DomainErrorCodes.EDAnakin1006), DomainErrorCodes.EDAnakin1006);

            Guard.That<DomainException>(string.IsNullOrEmpty(nocInformationCommand.PopName),
                nameof(DomainErrorCodes.EDAnakin1007), DomainErrorCodes.EDAnakin1007);

            Guard.That<DomainException>(string.IsNullOrEmpty(nocInformationCommand.Category),
                nameof(DomainErrorCodes.EDAnakin1008), DomainErrorCodes.EDAnakin1008);

            Guard.That<DomainException>(nocInformationCommand.DiscoveryDate == default(DateTime),
                nameof(DomainErrorCodes.EDAnakin1009), DomainErrorCodes.EDAnakin1009);

            var nocInformation = new NocInformation(
                nocInformationCommand.LogIndexName,
                nocInformationCommand.LogIndexId,
                nocInformationCommand.HostName,
                nocInformationCommand.Message,
                nocInformationCommand.LogDate,
                nocInformationCommand.PopName,
                nocInformationCommand.Category,
                nocInformationCommand.DiscoveryDate,
                nocInformationCommand.Description);

            nocInformation = await nocInformation.SetTicketIdAsync(ticketProxy, productProxy, cancellationToken);
            nocInformation.ChangeStatus("OPEN");

            return nocInformation;
        }


        /// <summary>
        /// this is factory method
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private NocInformation ChangeStatus(string status)
        {
            switch (status)
            {
                case "OPEN":
                    AddEvent(new NocInformationOpened(LogIndexId, LogIndexName));
                    break;
            }

            Status = status;
            return this;
        }

        public NocInformation SetCloseDescription(string description)
        {
            ClosingDescription = description?.Trim();
            ClosingDate = DateTime.Now;
            Updated = DateTime.Now;
            ChangeStatus("CLOSED");
            return this;
        }


        private async Task<NocInformation> SetTicketIdAsync(ITicketProxy ticketProxy, IProductProxy productProxy,
            CancellationToken cancellationToken)
        {
            var productId = await productProxy.GetProductIdByHostNameAsync(HostName);
            Guard.That<DomainException>(productId == 0,
                nameof(DomainErrorCodes.EDAnakin1014),
                DomainErrorCodes.EDAnakin1014);

            var ticketId = await ticketProxy.CreateTicketAsync(productId, Description);
            Guard.That<DomainException>(ticketId == 0,
                nameof(DomainErrorCodes.EDAnakin1015),
                DomainErrorCodes.EDAnakin1015);

            TicketId = ticketId;
            return this;
        }

        /// <summary>
        ///  this is factory method
        /// </summary>
        /// <param name="recoveryTime"></param>
        /// <returns></returns>
        public NocInformation SetRecoveryTime(DateTime? recoveryTime)
        {
            if (recoveryTime.HasValue)
                RecoveryTime = recoveryTime;

            return this;
        }


        /// <summary>
        ///  this is factory method
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public NocInformation AddDevice(string device)
        {
            if (DeviceList == null)
                DeviceList = new List<Device>();

            DeviceList.Add(Device.Load(device));
            Devices = DeviceList.Select(x => x.Name).ToArray();
            return this;
        }

        /// <summary>
        /// event raise edilebilir. Id ve  update description için !!
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        // public NocInformation AddLineItem(string description)
        // {
        //     if (LineItems == null)
        //         LineItems = new List<NocLineItem>();
        //
        //     LineItems.Add(NocLineItem.Load(description));
        //     return this;
        // }
    }
}