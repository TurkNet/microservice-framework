using System;
using System.Collections.Generic;

namespace Noctools.Elasticsearch
{
    public class ElasticsearchSettings
    {
        public Endpoint[] Endpoints { get; set; }
    }

    public class Endpoint
    {
        public string Uri { get; set; }
    }
}
