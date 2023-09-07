using Domain.Dtos.Requests.User;
using Domain.Dtos.Responses.User;

namespace Domain.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserFilteringResponse>> FilterAsync(UserFilteringRequest request);
    Task<UserFindByIdResponse> FindByIdAsync(UserFindByIdRequest resquest);
    Task<UserCreationResponse> CreateAsync(UserCreationRequest request);
    Task<UserUpdateResponse> UpdateAsync(UserUpdateRequest request);
    Task<UserDeleteResponse> DeleteAsync(UserDeleteRequest request);
}
