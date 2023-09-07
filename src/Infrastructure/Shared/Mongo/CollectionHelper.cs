using Domain.Shared.Mongo;
using MongoDB.Driver;

namespace Infrastructure.Shared.Mongo;

public static class CollectionHelper
{
    public static string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault())?.CollectionName;
    }
}
