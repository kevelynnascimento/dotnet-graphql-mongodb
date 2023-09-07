using Domain.Profiles;

namespace GraphQL.Config
{
    public static class Bootstrapper
    {
        public static void Bootstrap(this WebApplicationBuilder builder)
        {
            EnvBootstrapper.Load();

            builder.BootstrapGraphQL();

            builder.BootstrapLayers();

            builder.BootstrapMongoDB();

            builder.BootstrapValidators();

            builder.Services.AddHealthChecks();

            builder.Services.AddAutoMapper(typeof(UserProfile));

            builder.BootstrapHttpRepositories();

            var app = builder.Build();

            app.MapGraphQL(path: "/");

            app.MapHealthChecks("/health");

            app.Run();
        }
    }
}
