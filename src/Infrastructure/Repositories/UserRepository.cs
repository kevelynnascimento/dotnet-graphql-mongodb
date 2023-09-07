using Domain.Dtos.Requests.User;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Shared.Mongo.Interfaces;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoRepository<User> _repository;

    public UserRepository(IMongoRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<User>> FilterAsync(UserFilteringRequest request)
    {
        var (filter, builder) = _repository.BuildFilter();

        if (!string.IsNullOrWhiteSpace(request?.Name))
            filter &= builder.Eq(x => x.Name, request.Name);

        var users = await _repository.FilterAsync(filter, request);

        return users;
    }

    public async Task InsertAsync(User user)
    {
        await _repository.InsertAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await _repository.UpdateAsync(user);
    }

    public async Task<User> FindByIdAsync(string id)
    {
        var user = await _repository.FindByIdAsync(id);
        return user;
    }

    public async Task DeleteByIdAsync(string id)
    {
        await _repository.DeleteByIdAsync(id);
    }
}
