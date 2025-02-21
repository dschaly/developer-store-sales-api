using Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Branches;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branches;

/// <summary>
/// Contains unit tests for the <see cref="DeleteBranchHandler"/> class.
/// </summary>
public class DeleteBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly DeleteBranchHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBranchHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public DeleteBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _handler = new DeleteBranchHandler(_branchRepository);
    }

    /// <summary>
    /// Tests that a valid DeleteBranchCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid branch ID When deleting branch Then returns success")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Given
        var command = DeleteBranchHandlerTestData.GenerateValidCommand();
        _branchRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        await _branchRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid DeleteBranchCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid branch ID When deleting branch Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = DeleteBranchHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that deleting a non-existing branch throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing branch ID When deleting branch Then throws key not found exception")]
    public async Task Handle_NonExistingBranch_ThrowsKeyNotFoundException()
    {
        // Given
        var command = DeleteBranchHandlerTestData.GenerateValidCommand();
        _branchRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Branch with ID {command.Id} not found");
    }
}
