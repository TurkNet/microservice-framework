using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Noctools.Bus;
using Noctools.RestTemplate.Elasticsearch;
using Noctools.TnMon.Api.Infrastructure;

namespace Noctools.TnMon.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddElasticsearchTemplate();
            services.AddDomainEventBus();
            services.AddInfrastructure();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseElasticsearchTemplate();
        }
    }
}