using Domain.Shared.Entities;
using Domain.Shared.Pagination;
using MongoDB.Driver;

namespace Infrastructure.Shared.Mongo.Interfaces;

public interface IMongoRepository<T> where T : MongoEntity
{
    IMongoCollection<T> Collection { get; }
    Task<T> FindByIdAsync(string id);
    Task DeleteByIdAsync(string id);
    Task UpdateAsync(T entity);
    Task InsertAsync(T entity);
    (FilterDefinition<T>, FilterDefinitionBuilder<T>) BuildFilter();
    Task<IEnumerable<T>> FilterAsync(FilterDefinition<T> filter, PaginationRequest pagination);
}