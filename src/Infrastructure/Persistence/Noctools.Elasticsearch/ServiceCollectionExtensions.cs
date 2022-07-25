using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Noctools.Domain;
using System.Linq;
using System.Reflection;
using Noctools.Infrastructure;

namespace Noctools.Elasticsearch
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElasticsearch(this IServiceCollection services)
        {
            var svcProvider = services.BuildServiceProvider();
            var config = svcProvider.GetRequiredService<IConfiguration>();

            var entityTypes = config
                .LoadFullAssemblies()
                .SelectMany(m => m.DefinedTypes)
                .Where(x => typeof(IAggregateRoot).IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract);


            foreach (var entity in entityTypes)
            {
                var repoType = typeof(IRepositoryAsync<>).MakeGenericType(entity);
                var implRepoType = typeof(Repository<>).MakeGenericType(entity);
                services.AddScoped(repoType, implRepoType);

                var queryRepoType = typeof(IQueryRepository<>).MakeGenericType(entity);
                var implQueryRepoType = typeof(Repository<>).MakeGenericType(entity);
                services.AddScoped(queryRepoType, implQueryRepoType);
            }

            services.Configure<ElasticsearchSettings>(config.GetSection("Features:Elasticsearch"));
            services.AddScoped<DbContext>();


            services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>(); //command repository factory
            services.AddScoped<IQueryRepositoryFactory, QueryRepositoryFactory>(); // query repository factory

            return services;
        }
    }

    public static class ElasticsearchQueryRepositoryFactoryExtensions
    {
        public static IElasticsearchQueryRepository<TEntity> ElasticsearchQueryRepository<TEntity>(
            this IQueryRepositoryFactory factory)
            where TEntity : class, IAggregateRoot
        {
            return factory.QueryRepository<TEntity>() as IElasticsearchQueryRepository<TEntity>;
        }
    }
}