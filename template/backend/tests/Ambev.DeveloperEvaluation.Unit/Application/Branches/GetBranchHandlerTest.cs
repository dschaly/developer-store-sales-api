using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
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
/// Contains unit tests for the <see cref="GetBranchHandler"/> class.
/// </summary>
public class GetBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly GetBranchHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBranchHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public GetBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetBranchHandler(_branchRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid GetBranchCommand is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid branch ID When retrieving branch Then returns branch details")]
    public async Task Handle_ValidRequest_ReturnsBranchDetails()
    {
        // Given
        var command = GetBranchHandlerTestData.GenerateValidCommand();
        var branch = GetBranchHandlerTestData.GenerateValidBranch();
        var expectedResult = GetBranchHandlerTestData.GenerateValidResult();

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(branch);
        _mapper.Map<GetBranchResult>(branch).Returns(expectedResult);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedResult.Id);
        result.Name.Should().Be(expectedResult.Name);
        result.Address.Should().Be(expectedResult.Address);
        await _branchRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid GetBranchCommand throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid branch ID When retrieving branch Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetBranchCommand(Guid.Empty); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that retrieving a non-existing branch throws a KeyNotFoundException.
    /// </summary>
    [Fact(DisplayName = "Given non-existing branch ID When retrieving branch Then throws key not found exception")]
    public async Task Handle_NonExistingBranch_ThrowsKeyNotFoundException()
    {
        // Given
        var command = GetBranchHandlerTestData.GenerateValidCommand();

        _branchRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Branch with ID {command.Id} not found");
    }
}
