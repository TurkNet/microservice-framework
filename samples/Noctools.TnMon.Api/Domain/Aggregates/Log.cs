using System;
using System.Collections.Generic;
using Nest;
using Noctools.Domain;
using Noctools.TnMon.Api.Contants;

namespace Noctools.TnMon.Api.Domain
{
    [ElasticsearchType(Name = ApplicationConstants.LogIndexType)]
    public class Log : AggregateRootBase
    {
        [Text(Ignore = true)] public new string Id { get; set; }
        [Text(Ignore = true)] public string Index { get; set; }
        [Text(Name = "syslog_hostname")] public string HostName { get; set; }
        [Text(Name = "speed")] public string Speed { get; set; }
        [Text(Name = "threshold")] public string Threshold { get; set; }
        [Text(Name = "@timestamp")] public DateTime TimeStamp { get; set; }
        [Text(Name = "alertfilter")] public int AlertFilter { get; set; }
        [Text(Name = "alerttype")] public int AlertType { get; set; }
        [Text(Name = "alertrule")] public string AlertRule { get; set; }
        [Text(Name = "message")] public string Message { get; set; }
        [Text(Name = "nocInformation")] public string NocInformation { get; set; }

        /// <summary>
        /// impedance mismatch for newtonsoft json
        /// </summary>
        public Log()
        {
        }

        public static Log Load()
        {
            return new Log();
        }

        public Log SetDescription(string description)
        {
            NocInformation = description;
            return this;
        }

        public Log UpToDate()
        {
            Updated = DateTimeOffset.Now.DateTime;
            Created = TimeStamp;
            return this;
        }


        public Log SetId(string id)
        {
            Id = id;
            return this;
        }

        public Log SetIndex(string index)
        {
            Index = index;
            return this;
        }
    }
}