using AutoMapper;
using Domain.Dtos.Requests.User;
using Domain.Dtos.Responses.User;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services.Interfaces;
using Domain.Shared.Exceptions;
using Domain.Shared.Extensions;
using FluentValidation;

namespace Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreationRequest> _userCreationRequestValidator;
    private readonly IMapper _mapper;
    private readonly IValidator<UserUpdateRequest> _userUpdateRequestValidator;
    private readonly IValidator<UserFindByIdRequest> _userFindByIdResquestValidator;

    public UserService
    (
        IUserRepository userRepository,
        IValidator<UserCreationRequest> userCreationValidator,
        IMapper mapper,
        IValidator<UserUpdateRequest> userUpdateValidator,
        IValidator<UserFindByIdRequest> userFindByIdResquestValidator
    )
    {
        _userRepository = userRepository;
        _userCreationRequestValidator = userCreationValidator;
        _mapper = mapper;
        _userUpdateRequestValidator = userUpdateValidator;
        _userFindByIdResquestValidator = userFindByIdResquestValidator;
    }

    public async Task<IEnumerable<UserFilteringResponse>> FilterAsync(UserFilteringRequest request)
    {
        var users = await _userRepository.FilterAsync(request);

        var response = _mapper.Map<IEnumerable<UserFilteringResponse>>(users);

        return response;
    }

    public async Task<UserFindByIdResponse> FindByIdAsync(UserFindByIdRequest request)
    {
        var validation = _userFindByIdResquestValidator.Validate(request);

        if (!validation.IsValid)
            throw new DomainException(validation.GetErrorMessage());

        var user = await _userRepository.FindByIdAsync(request.Id);

        var response = _mapper.Map<UserFindByIdResponse>(user);

        return response;
    }

    public async Task<UserCreationResponse> CreateAsync(UserCreationRequest request)
    {
        var validation = _userCreationRequestValidator.Validate(request);

        if (!validation.IsValid)
            throw new DomainException(validation.GetErrorMessage());

        var user = new User(request.Name);

        await _userRepository.InsertAsync(user);

        var response = _mapper.Map<UserCreationResponse>(user);

        return response;
    }

    public async Task<UserUpdateResponse> UpdateAsync(UserUpdateRequest request)
    {
        var validation = _userUpdateRequestValidator.Validate(request);

        if (!validation.IsValid)
            throw new DomainException(validation.GetErrorMessage());

        var user = await _userRepository.FindByIdAsync(request.Id);

        if (user == null)
            return null;

        user.Name = request.Name;

        await _userRepository.UpdateAsync(user);

        var response = _mapper.Map<UserUpdateResponse>(user);

        return response;
    }

    public async Task<UserDeleteResponse> DeleteAsync(UserDeleteRequest request)
    {
        var user = await _userRepository.FindByIdAsync(request.Id);

        if (user == null)
            return null;

        await _userRepository.DeleteByIdAsync(user.Id);

        var response = new UserDeleteResponse
        {
            Success = true
        };

        return response;
    }
}
