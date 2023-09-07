using Domain.Services;
using Infrastructure.Repositories;

namespace GraphQL.Config;

public static class LayersBootstraper
{
    public static void BootstrapLayers(this WebApplicationBuilder builder)
    {
        builder.Services
          .Scan(scan => scan.FromAssemblyOf<UserService>()
          .AddClasses()
          .AsImplementedInterfaces()
          .WithScopedLifetime());

        builder.Services
            .Scan(scan => scan.FromAssemblyOf<UserRepository>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}
