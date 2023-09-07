using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Shared.Entities;

[ExcludeFromCodeCoverage]
public class MongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
}
