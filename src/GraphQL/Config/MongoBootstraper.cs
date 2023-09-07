using Infrastructure.Shared.Mongo;
using Infrastructure.Shared.Mongo.Interfaces;

namespace GraphQL.Config;

public static class MongoBootstraper
{
    public static void BootstrapMongoDB(this WebApplicationBuilder builder)
    {
        var databaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME");
        var connectionString = Environment.GetEnvironmentVariable("MONGO_DATABASE_CONNECTION_STRING");

        var mongoDbSettings = new MongoDbSettings
        {
            DatabaseName = databaseName,
            ConnectionString = connectionString
        };

        builder.Services.AddSingleton<IMongoDbSettings>(mongoDbSettings);

        builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    }
}
