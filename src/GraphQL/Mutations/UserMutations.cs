using Domain.Dtos.Requests.User;
using Domain.Dtos.Responses.User;
using Domain.Services.Interfaces;

namespace GraphQL.Mutations;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class UserMutations
{
    private readonly IUserService _userService;

    public UserMutations(IUserService userService)
    {
        _userService = userService;
    }

    [GraphQLName("createUser")]
    public async Task<UserCreationResponse> CreateAsync(UserCreationRequest request)
    {
        var response = await _userService.CreateAsync(request);
        return response;
    }

    [GraphQLName("updateUser")]
    public async Task<UserUpdateResponse> UpdateAsync(UserUpdateRequest request)
    {
        var response = await _userService.UpdateAsync(request);
        return response;
    }

    [GraphQLName("deleteUser")]
    public async Task<UserDeleteResponse> DeleteAsync(UserDeleteRequest request)
    {
        var response = await _userService.DeleteAsync(request);
        return response;
    }
}