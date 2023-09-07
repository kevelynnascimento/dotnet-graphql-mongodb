using Domain.Shared.Entities;
using Domain.Shared.Pagination;
using Infrastructure.Shared.Mongo.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Shared.Mongo;

public class MongoRepository<T> : IMongoRepository<T> where T : MongoEntity
{
    public IMongoCollection<T> collection;

    public MongoRepository(IMongoDbSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        collection = database.GetCollection<T>(CollectionHelper.GetCollectionName(typeof(T)));
    }

    public IMongoCollection<T> Collection => collection;

    public async Task<T> FindByIdAsync(string id)
    {
        var query = await collection.FindAsync(x => x.Id == id);

        var entity = await query.FirstOrDefaultAsync();

        return entity;
    }

    public async Task DeleteByIdAsync(string id)
    {
        await collection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task InsertAsync(T entity)
    {
        await collection.InsertOneAsync(entity);
    }

    public (FilterDefinition<T>, FilterDefinitionBuilder<T>) BuildFilter()
    {
        var builder = Builders<T>.Filter;

        var filter = builder.Empty;

        return (filter, builder);
    }

    public async Task<IEnumerable<T>> FilterAsync(FilterDefinition<T> filter, PaginationRequest pagination)
    {
        var entities = await collection
            .Find(filter)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Limit(pagination.PageSize)
            .ToListAsync();

        return entities;
    }
}
