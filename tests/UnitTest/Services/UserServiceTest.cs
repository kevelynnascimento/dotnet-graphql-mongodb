using AutoMapper;
using Domain.Dtos.Requests.User;
using Domain.Dtos.Responses.User;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Domain.Shared.Exceptions;
using FizzWare.NBuilder;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace UnitTest.Services;

public class UserServiceTest
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IValidator<UserCreationRequest>> _userCreationRequestValidatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<UserUpdateRequest>> _userUpdateRequestValidatorMock;
    private readonly Mock<IValidator<UserFindByIdRequest>> _userFindByIdRequestValidatorMock;

    public UserServiceTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userCreationRequestValidatorMock = new Mock<IValidator<UserCreationRequest>>();
        _mapperMock = new Mock<IMapper>();
        _userUpdateRequestValidatorMock = new Mock<IValidator<UserUpdateRequest>>();
        _userFindByIdRequestValidatorMock = new Mock<IValidator<UserFindByIdRequest>>();

        _userService = new UserService(
            _userRepositoryMock.Object,
            _userCreationRequestValidatorMock.Object,
            _mapperMock.Object,
            _userUpdateRequestValidatorMock.Object,
            _userFindByIdRequestValidatorMock.Object
        );
    }

    public class FilterAsync : UserServiceTest
    {
        [Fact]
        public async Task FilterAsync_ReturnFilteredUsers()
        {
            var request = Builder<UserFilteringRequest>.CreateNew().Build();
            var users = Builder<User>.CreateListOfSize(5).All().WithFactory(() => new User("user")).Build();
            var expectedResponse = Builder<UserFilteringResponse>.CreateListOfSize(5).Build();

            _userRepositoryMock.Setup(x => x.FilterAsync(request)).ReturnsAsync(users);
            _mapperMock.Setup(x => x.Map<IEnumerable<UserFilteringResponse>>(users)).Returns(expectedResponse);

            var response = await _userService.FilterAsync(request);

            response.Should().BeEquivalentTo(expectedResponse);

            _userRepositoryMock.Verify(x => x.FilterAsync(It.IsAny<UserFilteringRequest>()), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<UserFilteringResponse>>(It.IsAny<IList<User>>()), Times.Once);
        }
    }

    public class FindByIdAsync : UserServiceTest
    {
        [Fact]
        public async Task FindByIdAsync_ReturnUserFindByIdResponseWithValidRequest()
        {
            var request = Builder<UserFindByIdRequest>.CreateNew().Build();
            var validation = Builder<ValidationResult>.CreateNew().Build();
            var expectedResponse = Builder<UserFindByIdResponse>.CreateNew().Build();

            var user = Builder<User>.CreateNew().WithFactory(() => new User("user")).Build();

            _userFindByIdRequestValidatorMock.Setup(x => x.Validate(request)).Returns(validation);
            _userRepositoryMock.Setup(x => x.FindByIdAsync(request.Id)).ReturnsAsync(user);
            _mapperMock.Setup(x => x.Map<UserFindByIdResponse>(user)).Returns(expectedResponse);

            var response = await _userService.FindByIdAsync(request);

            response.Should().BeEquivalentTo(expectedResponse);

            _userFindByIdRequestValidatorMock.Verify(x => x.Validate(It.IsAny<UserFindByIdRequest>()), Times.Once);
            _userRepositoryMock.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _mapperMock.Verify(x => x.Map<UserFindByIdResponse>(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task FindByIdAsync_ReturnUserFindByIdResponseWithInvalidRequest()
        {
            var request = Builder<UserFindByIdRequest>.CreateNew().Build();
            var erros = Builder<ValidationFailure>.CreateListOfSize(2).Build();
            var validation = Builder<ValidationResult>.CreateNew().With(x => x.Errors = erros.ToList()).Build();
            var expectedResponse = Builder<UserFindByIdResponse>.CreateNew().Build();

            var user = Builder<User>.CreateNew().WithFactory(() => new User("user")).Build();

            _userFindByIdRequestValidatorMock.Setup(x => x.Validate(request)).Returns(validation); ;

            await Assert.ThrowsAsync<DomainException>(async () => await _userService.FindByIdAsync(request));

            _userFindByIdRequestValidatorMock.Verify(x => x.Validate(It.IsAny<UserFindByIdRequest>()), Times.Once);
            _userRepositoryMock.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(x => x.Map<UserFindByIdResponse>(It.IsAny<User>()), Times.Never);
        }
    }

    public class CreateAsync : UserServiceTest
    {
        [Fact]
        public async Task CreateAsync_WithValidRequest_ShouldCreateUserAndReturnResponse()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Name = "Wan"
            };

            var user = new User(request.Name);
            var expectedResponse = new UserCreationResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name
            };

            _userCreationRequestValidatorMock.Setup(x => x.Validate(request)).Returns(new ValidationResult());
            _userRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _mapperMock.Setup(x => x.Map<UserCreationResponse>(It.IsAny<User>())).Returns(expectedResponse);

            // Act
            var response = await _userService.CreateAsync(request);

            // Assert
            response.Should().BeEquivalentTo(expectedResponse);

            _userCreationRequestValidatorMock.Verify(x => x.Validate(It.IsAny<UserCreationRequest>()), Times.Once);
            _userRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<User>()), Times.Once);
            _mapperMock.Verify(x => x.Map<UserCreationResponse>(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithInvalidRequest_ShouldThrowDomainException()
        {
            // Arrange
            var request = new UserCreationRequest
            {
                Name = "Wan"
            };

            var validationErrors = new ValidationResult(new ValidationFailure[]
            {
                new ValidationFailure("Name", "Name is required.")
            });

            _userCreationRequestValidatorMock.Setup(x => x.Validate(request)).Returns(validationErrors);

            // Act
            await Assert.ThrowsAsync<DomainException>(async () => await _userService.CreateAsync(request));

            // Assert
            _userCreationRequestValidatorMock.Verify(x => x.Validate(It.IsAny<UserCreationRequest>()), Times.Once);
            _userRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<User>()), Times.Never);
            _mapperMock.Verify(x => x.Map<UserCreationResponse>(It.IsAny<User>()), Times.Never);
        }
    }

    public class UpdateAsync : UserServiceTest
    {
        [Fact]
        public async Task UpdateAsync_WithValidRequest_ShouldUpdateUserAndReturnNullResponse()
        {
            // Arrange
            var request = new UserUpdateRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Wan"
            };

            var expectedResponse = new UserUpdateResponse
            {
                Id = request.Id,
                Name = request.Name
            };

            _userUpdateRequestValidatorMock.Setup(x => x.Validate(request)).Returns(new ValidationResult());
            _userRepositoryMock.Setup(x => x.FindByIdAsync(request.Id)).ReturnsAsync((User?)null);
            _userRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _mapperMock.Setup(x => x.Map<UserUpdateResponse>(It.IsAny<User>())).Returns(expectedResponse);

            // Act
            var response = await _userService.UpdateAsync(request);

            // Assert
            response.Should().BeNull();
            _userRepositoryMock.Verify(x => x.FindByIdAsync(request.Id), Times.Once);
            _userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WithValidRequest_ShouldUpdateUserAndReturnResponse()
        {
            // Arrange
            var request = new UserUpdateRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Wan"
            };

            var existingUser = new User("Wan")
            {
                Id = request.Id
            };

            var expectedResponse = new UserUpdateResponse
            {
                Id = request.Id,
                Name = request.Name
            };

            _userUpdateRequestValidatorMock.Setup(x => x.Validate(request)).Returns(new ValidationResult());
            _userRepositoryMock.Setup(x => x.FindByIdAsync(request.Id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _mapperMock.Setup(x => x.Map<UserUpdateResponse>(It.IsAny<User>())).Returns(expectedResponse);

            // Act
            var response = await _userService.UpdateAsync(request);

            // Assert
            response.Should().BeEquivalentTo(expectedResponse);

            _userUpdateRequestValidatorMock.Verify(x => x.Validate(It.IsAny<UserUpdateRequest>()), Times.Once);
            _userRepositoryMock.Verify(x => x.FindByIdAsync(request.Id), Times.Once);
            _userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
            _mapperMock.Verify(x => x.Map<UserUpdateResponse>(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithInvalidRequest_ShouldThrowDomainException()
        {
            // Arrange
            var request = new UserUpdateRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Wan"
            };

            var validationErrors = new ValidationResult(new ValidationFailure[]
            {
                new ValidationFailure("Name", "Name is required.")
            });

            _userUpdateRequestValidatorMock.Setup(x => x.Validate(request)).Returns(validationErrors);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(async () => await _userService.UpdateAsync(request));

            _userUpdateRequestValidatorMock.Verify(x => x.Validate(It.IsAny<UserUpdateRequest>()), Times.Once);
            _userRepositoryMock.Verify(x => x.FindByIdAsync(It.IsAny<Guid>().ToString()), Times.Never);
            _userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
            _mapperMock.Verify(x => x.Map<UserUpdateResponse>(It.IsAny<User>()), Times.Never);
        }
    }

    public class DeleteAsync : UserServiceTest
    {
        [Fact]
        public async Task DeleteAsync_WithExistingUserId_ShouldDeleteUserAndReturnResponse()
        {
            // Arrange
            var request = new UserDeleteRequest
            {
                Id = Guid.NewGuid().ToString()
            };

            var existingUser = new User("Wan")
            {
                Id = request.Id
            };

            _userRepositoryMock.Setup(x => x.FindByIdAsync(request.Id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(x => x.DeleteByIdAsync(request.Id)).Returns(Task.CompletedTask);

            // Act
            var response = await _userService.DeleteAsync(request);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();

            _userRepositoryMock.Verify(x => x.FindByIdAsync(request.Id), Times.Once);
            _userRepositoryMock.Verify(x => x.DeleteByIdAsync(request.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithNonExistingUserId_ShouldReturnNull()
        {
            // Arrange
            var request = new UserDeleteRequest
            {
                Id = Guid.NewGuid().ToString(),
            };

            _userRepositoryMock.Setup(x => x.FindByIdAsync(request.Id)).ReturnsAsync((User?)null);

            // Act
            var response = await _userService.DeleteAsync(request);

            // Assert
            response.Should().BeNull();

            _userRepositoryMock.Verify(x => x.FindByIdAsync(request.Id), Times.Once);
            _userRepositoryMock.Verify(x => x.DeleteByIdAsync(It.IsAny<Guid>().ToString()), Times.Never);
        }
    }
}
