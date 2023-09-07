namespace GraphQL.Config
{
    public static class HttpBootstraper
    {
        public static void BootstrapHttpRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("external-api", x =>
            {
                x.BaseAddress = new Uri("https://external.com/api");
            });
        }
    }
}
