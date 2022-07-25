using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Noctools.Domain;
using Noctools.Utils.Extensions;

namespace Noctools.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static IEnumerable<Assembly> LoadFullAssemblies(this IConfiguration config)
        {
            if (string.IsNullOrEmpty(config.GetValue<string>("QualifiedAssemblyPattern")))
                throw new CoreException(
                    "Add QualifiedAssemblyPattern key in appsettings.json for automatically loading assembly.");

            return config.GetValue<string>("QualifiedAssemblyPattern").LoadFullAssemblies();
        }

        public static IEnumerable<Assembly> LoadApplicationAssemblies(this IConfiguration config)
        {
            if (string.IsNullOrEmpty(config.GetValue<string>("QualifiedAssemblyPattern")))
                throw new CoreException(
                    "Add QualifiedAssemblyPattern key in appsettings.json for automatically loading assembly.");

            var apps = config.GetValue<string>("QualifiedAssemblyPattern").LoadAssemblyWithPattern();
            if (apps == null || !apps.Any())
                throw new Exception("Should have at least one application assembly to load.");

            return apps;
        }
    }

    public class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration(string basePath = null)
        {
            basePath = basePath ?? Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}