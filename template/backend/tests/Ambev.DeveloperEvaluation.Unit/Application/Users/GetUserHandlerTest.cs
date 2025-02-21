using Ambev.DeveloperEvaluation.Application.Users.GetUser;
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
/// Contains unit tests for the <see cref="GetUserHandler"/> class.
/// </summary>
public class GetUserHandlerTests
{
    private readonly IUserRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly GetUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetUserHandlerTests()
    {
        _branchRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetUserHandler(_branchRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid GetUserCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid branch ID When retrieving branch Then returns branch details")]
    public async Task Handle_ValidRequest_ReturnsUserDetails()
    {
        // Given
        var command = GetUserHandlerTestData.GenerateValidCommand();
        var branch = GetUserHandlerTestData.GenerateValidUser();
        var expectedResult = GetUserHandlerTestData.GenerateValidResult();

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(branch);
        _mapper.Map<GetUserResult>(branch).Returns(expectedResult);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.Name.Should().Be(expectedResult.Name);
        result.Email.Should().Be(expectedResult.Email);
        await _branchRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid GetUserCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid branch ID When retrieving branch Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetUserCommand(Guid.Empty); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that retrieving a non-existing branch throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing branch ID When retrieving branch Then throws key not found exception")]
    public async Task Handle_NonExistingUser_ThrowsKeyNotFoundException()
    {
        // Given
        var command = GetUserHandlerTestData.GenerateValidCommand();

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID {command.Id} not found");
    }
}
