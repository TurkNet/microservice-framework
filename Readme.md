# Cloud-native Noctools Core Kit

A set of cloud-native tools and utilities for .NET Core.

The goal of this project is implement the most common used cloud-native technologies (cloud-agnostic approach, containerization mechanism, container orchestration and so on) and share with the technical community the best way to develop great applications with .NET Core.

### Features

- [x] Simple libraries. No frameworks. Little abstraction.
- [x] Opt-in and out of the box [features](https://github.com/cloudnative-netcore/netcorekit/wiki/Host-template-guidance) with [Feature Toggles](https://martinfowler.com/articles/feature-toggles.html) technique.
- [x] Adhere to [twelve-factor app paradigm](https://12factor.net) and more.
- [x] Authentication/Authorization with OAuth 2.0 and OpenID Connect.
- [x] Out of the box Miniprofiler.
- [x] [Domain-driven Design](https://en.wikipedia.org/wiki/Domain-driven_design) in mind.
- [x] Simply [Clean Architecture](http://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) supports.
- [x] Generic repository for data persistence.
- [x] Mapping between domain entity to DTO and vice versa.
- [x] Clean and demystify error, debug logs.
- [x] Resilience and health check out of the box.
- [x] Easy for configuration management.
- [x] API versioning from Docker container to WebAPI.
- [x] Documentation template with OpenAPI documentation.
- [ ] Cache a side 
- [ ] Wcf Proxy 
- [ ] Message Broker
- [ ] Unit and integration tests
- [ ] Sonar Cloud
- [ ] Arch Test

### Features Devops

- [ ] Work natively with Kubernetes or even with Service Mesh(Istio).
- [x] Logging
- [ ] Dashboard on Kibana like (pagespeed,errorline,response time etc.)
- [ ] Monitoring
- [ ] Alert notification on monitoring systems like grafana
- [ ] Obversability like zipkin
- [ ] Correlation 


## Less code to get starting

Small, lightweight, cloud-native out of the box, and much more simple to get starting with miniservices approach. [Why miniservices?](https://thenewstack.io/miniservices-a-realistic-alternative-to-microservices)

### Look how simple we can start as below:

- **Standard template - Noctools.Template.Standard**: without storage, merely calculation and job tasks:

```csharp
public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddStandardTemplate();
  }

  public void Configure(IApplicationBuilder app)
  {
    app.UseStandardTemplate();
  }
}
```

- **Elasticsearch template - Noctools.Template.Elasticsearch**: with NoSQL (Elasticsearch provider) comes along with the generic repository in place:

```csharp
public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddElasticsearchTemplate();
  }

  public void Configure(IApplicationBuilder app)
  {
    app.UseElasticsearchTemplate();
  }
}
```

### Host template guidance

- [x] Standard Template
- [x] Elasticsearch Template
- [x] EfCore Template 
- [ ] MongoDB Template 


### Nuget package publish
```sh
$ dotnet pack <.csproj path> -c Release
$ nuget push -Source https://pkgs.dev.azure.com/turknet-it/_packaging/turknet-it/nuget/v3/index.json -ApiKey az <.nupkg>
$ username : from nuget.config
$ password : from nuget.config

```

Open https://dev.azure.com/turknet-it/Tn.Nuget/_packaging?_a=feed&feed=turknet-it
and search as noctools on filter bar


### Application architecture
![app_architecture](images/ddd-microservice-simple.png 'app_architecture')


### Microservice architecture

![msa_architecture](images/msa_architecture.png 'msa_architecture')

### Domain architecture

![msa_architecture](images/definitions_and_patterns.png 'definitions_and_patterns')

## Devops architecture
### cloud native roadmap

<img src="https://raw.githubusercontent.com/lakwarus/reference-architecture/master/media/ra-cloud-native-architecture-for-a-digital-enterprise.png" alt="cloud-native"> 


### gitops roadmap

<img src="https://raw.githubusercontent.com/lakwarus/reference-architecture/master/media/ra-gitops.png" alt="gitops">

### Clean ddd in 10 minutes

1. https://medium.com/hackernoon/clean-domain-driven-design-in-10-minutes-6037a59c8b7b 

### Books

1. https://www.amazon.com.tr/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215
2. https://www.amazon.com/Implementing-Domain-Driven-Design-Vaughn-Vernon/dp/0321834577
3. https://www.amazon.com.tr/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164
4. https://www.amazon.com/Microservices-Patterns-examples-Chris-Richardson/dp/1617294543

