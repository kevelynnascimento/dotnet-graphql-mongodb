using Domain.Shared.Entities;
using Domain.Shared.Mongo;

namespace Domain.Entities;

[BsonCollectionAttribute("user")]
public class User : MongoEntity
{
    public string Name { get; set; }

    public User(string name)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
    }
}
