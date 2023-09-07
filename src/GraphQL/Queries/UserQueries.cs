using Domain.Dtos.Requests.User;
using Domain.Dtos.Responses.User;
using Domain.Services.Interfaces;

namespace GraphQL.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public class UserQueries
{
    private readonly IUserService _userService;

    public UserQueries(IUserService userService)
    {
        _userService = userService;
    }

    [GraphQLName("getUsers")]
    public async Task<IEnumerable<UserFilteringResponse>> FilterAsync(UserFilteringRequest request)
    {
        var users = await _userService.FilterAsync(request);
        return users;
    }

    [GraphQLName("getUserById")]
    public async Task<UserFindByIdResponse> FindByIdAsync(UserFindByIdRequest resquest)
    {
        var user = await _userService.FindByIdAsync(resquest);
        return user;
    }
}