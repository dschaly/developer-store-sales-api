using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Auth;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth;

/// <summary>
/// Contains unit tests for the <see cref="AuthenticateUserHandler"/> class.
/// </summary>
public class AuthenticateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly AuthenticateUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public AuthenticateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _handler = new AuthenticateUserHandler(_userRepository, _passwordHasher, _jwtTokenGenerator);
    }

    /// <summary>
    /// Tests that a valid user authentication request returns the correct token and user details.
    /// </summary>
    [Fact(DisplayName = "Given valid credentials When authenticating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = AuthenticateHandlerTestData.GenerateValidCommand();
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            Password = command.Password,
            CreatedBy = "testUser",
            Status = UserStatus.Active,
        };

        var result = new AuthenticateUserResult
        {
            Token = "validToken",
            Email = user.Email,
            Name = user.Username,
            Role = user.Role.ToString()
        };

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);
        _jwtTokenGenerator.GenerateToken(user).Returns(result.Token);

        // When
        var authenticateResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        authenticateResult.Should().NotBeNull();
        authenticateResult.Token.Should().Be(result.Token);
        authenticateResult.Email.Should().Be(user.Email);
        authenticateResult.Name.Should().Be(user.Username);
        authenticateResult.Role.Should().Be(user.Role.ToString());
        await _userRepository.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid user authentication request throws an UnauthorizedAccessException.
    /// </summary>
    [Fact(DisplayName = "Given invalid credentials When authenticating user Then throws UnauthorizedAccessException")]
    public async Task Handle_InvalidCredentials_ThrowsUnauthorizedAccessException()
    {
        // Given
        var command = AuthenticateHandlerTestData.GenerateValidCommand();
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            Password = "incorrectPassword", // Invalid password
            CreatedBy = "testUser"
        };

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(false); // Password mismatch

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Invalid credentials");
    }

    /// <summary>
    /// Tests that the token generator is called to generate a token after successful authentication.
    /// </summary>
    [Fact(DisplayName = "Given valid user When authenticating Then generates JWT token")]
    public async Task Handle_ValidRequest_GeneratesToken()
    {
        // Given
        var command = AuthenticateHandlerTestData.GenerateValidCommand();
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            Password = command.Password,
            CreatedBy = "testUser",
            Status = UserStatus.Active,
        };

        var validator = new AuthenticateUserValidator();

        const string token = "validToken";

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);
        _jwtTokenGenerator.GenerateToken(user).Returns(token);

        // When
        var validationResponse = validator.Validate(command);
        await _handler.Handle(command, CancellationToken.None);

        // Then
        Assert.True(validationResponse.IsValid);
        _jwtTokenGenerator.Received(1).GenerateToken(user);
    }
}