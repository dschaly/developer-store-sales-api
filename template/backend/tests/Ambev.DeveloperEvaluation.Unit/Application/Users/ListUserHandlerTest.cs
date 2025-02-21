using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Users;
using AutoMapper;
using FluentAssertions;
using MockQueryable;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

/// <summary>
/// Contains unit tests for the <see cref="ListUserHandler"/> class.
/// </summary>
public class ListUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ListUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListUserHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public ListUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListUserHandler(_userRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid ListUserCommand returns a paginated user list.
    /// </summary>
    [Fact(DisplayName = "Given valid command When listing users Then returns paginated result")]
    public async Task Handle_ValidRequest_ReturnsPaginatedUsers()
    {
        // Given
        var command = ListUserHandlerTestData.GenerateValidCommand();
        var users = ListUserHandlerTestData.GenerateUserList();
        var mappedUsers = ListUserHandlerTestData.GenerateMappedUserList();

        var queryableUsers = users.AsQueryable().BuildMock(); // Using MockQueryable

        _userRepository.Query(Arg.Any<CancellationToken>()).Returns(queryableUsers);
        _mapper.Map<IEnumerable<UserResult>>(Arg.Any<IEnumerable<User>>()).Returns(mappedUsers);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.TotalCount.Should().Be(users.Count);
        result.CurrentPage.Should().Be(command.Page ?? 1);
        result.TotalPages.Should().Be((int)Math.Ceiling(users.Count / (double)(command.Size ?? 10)));

        await _userRepository.Received(1).Query(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid ListUserCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid command When listing users Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = ListUserHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    /// <summary>
    /// Tests that an empty user list returns an empty result.
    /// </summary>
    [Fact(DisplayName = "Given no users in repository When listing users Then returns empty result")]
    public async Task Handle_NoUsers_ReturnsEmptyResult()
    {
        // Given
        var command = ListUserHandlerTestData.GenerateValidCommand();
        var emptyQuery = Enumerable.Empty<User>().AsQueryable().BuildMock();

        _userRepository.Query(Arg.Any<CancellationToken>()).Returns(emptyQuery);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Data.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
        result.CurrentPage.Should().Be(command.Page ?? 1);
        result.TotalPages.Should().Be(0);
    }
}