using System;
using MediatR;
using Noctools.Domain;

namespace Noctools.TnMon.Api.Domain
{
    public class NocInformationOpened : EventBase, INotification
    {
        public string Id { get; private set; }
        public string Index { get; private set; }

        public NocInformationOpened(string id, string index)
        {
            Id = id;
            Index = index;
        }
    }
}