using Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Branches;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branches;

/// <summary>
/// Contains unit tests for the <see cref="UpdateBranchHandler"/> class.
/// </summary>
public class UpdateBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly UpdateBranchHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBranchHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();

        _handler = new UpdateBranchHandler(_branchRepository, _mapper, _user);
    }

    /// <summary>
    /// Tests that a valid UpdateBranchCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid update command When updating branch Then returns updated branch details")]
    public async Task Handle_ValidRequest_ReturnsUpdatedBranchDetails()
    {
        // Given
        var command = UpdateBranchHandlerTestData.GenerateValidCommand();
        var existingBranch = UpdateBranchHandlerTestData.GenerateExistingBranch();
        var updatedBranch = UpdateBranchHandlerTestData.GenerateUpdatedBranch();
        var expectedResult = UpdateBranchHandlerTestData.GenerateValidResult();

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingBranch);
        _mapper.Map(command, existingBranch).Returns(updatedBranch);
        _user.Username.Returns("admin");
        _branchRepository.UpdateAsync(existingBranch, Arg.Any<CancellationToken>()).Returns(updatedBranch);
        _mapper.Map<UpdateBranchResult>(updatedBranch).Returns(expectedResult);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);
        var validationResult = command.Validate();

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.Name.Should().Be(expectedResult.Name);
        result.Address.Should().Be(expectedResult.Address);

        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
        await _branchRepository.Received(1).UpdateAsync(existingBranch, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid UpdateBranchCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid update command When updating branch Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = UpdateBranchHandlerTestData.GenerateInvalidCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that updating a non-existing branch throws an InvalidOperationException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing branch ID When updating branch Then throws invalid operation exception")]
    public async Task Handle_NonExistingBranch_ThrowsInvalidOperationException()
    {
        // Given
        var command = UpdateBranchHandlerTestData.GenerateValidCommand();

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Entity with id {command.Id} not found");
    }
}