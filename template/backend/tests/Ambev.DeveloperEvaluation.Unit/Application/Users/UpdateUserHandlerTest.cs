using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Users;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

/// <summary>
/// Contains unit tests for the <see cref="UpdateUserHandler"/> class.
/// </summary>
public class UpdateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly UpdateUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();

        _handler = new UpdateUserHandler(_userRepository, _mapper, _user, _passwordHasher);
    }

    /// <summary>
    /// Tests that a valid UpdateUserCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid update command When updating user Then returns updated user details")]
    public async Task Handle_ValidRequest_ReturnsUpdatedUserDetails()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var existingUser = UpdateUserHandlerTestData.GenerateExistingUser();
        var updatedUser = UpdateUserHandlerTestData.GenerateUpdatedUser();
        var expectedResult = UpdateUserHandlerTestData.GenerateValidResult();

        _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingUser);
        _mapper.Map(command, existingUser).Returns(updatedUser);
        _user.Username.Returns("admin");
        _userRepository.UpdateAsync(existingUser, Arg.Any<CancellationToken>()).Returns(updatedUser);
        _mapper.Map<UpdateUserResult>(updatedUser).Returns(expectedResult);

        // When
        var validationResponse = command.Validate();
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.Username.Should().Be(expectedResult.Username);
        result.Email.Should().Be(expectedResult.Email);

        Assert.True(validationResponse.IsValid);
        Assert.Empty(validationResponse.Errors);
        await _userRepository.Received(1).UpdateAsync(existingUser, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid UpdateUserCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid update command When updating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that updating a non-existing user throws an InvalidOperationException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing user ID When updating user Then throws invalid operation exception")]
    public async Task Handle_NonExistingUser_ThrowsInvalidOperationException()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();

        _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Entity with id {command.Id} not found");
    }
}