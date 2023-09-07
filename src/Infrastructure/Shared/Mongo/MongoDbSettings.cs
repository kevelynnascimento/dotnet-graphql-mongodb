using Infrastructure.Shared.Mongo.Interfaces;

namespace Infrastructure.Shared.Mongo;

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}
