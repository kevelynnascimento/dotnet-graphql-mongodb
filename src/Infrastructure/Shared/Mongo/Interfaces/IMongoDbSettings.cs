namespace Infrastructure.Shared.Mongo.Interfaces;

public interface IMongoDbSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
}