using Domain.Dtos.Requests.User;
using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> FilterAsync(UserFilteringRequest request);
    Task InsertAsync(User user);
    Task UpdateAsync(User user);
    Task<User> FindByIdAsync(string id);
    Task DeleteByIdAsync(string id);
}
