using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Branches;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branches;

/// <summary>
/// Contains unit tests for the <see cref="CreateBranchHandler"/> class.
/// </summary>
public class CreateBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly CreateBranchHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBranchHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _mapper = Substitute.For<IMapper>();
        _user = Substitute.For<IUser>();
        _handler = new CreateBranchHandler(_branchRepository, _mapper, _user);
    }

    /// <summary>
    /// Tests that a valid branch creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid branch data When creating branch Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            CreatedBy = "testUser"
        };

        var result = new CreateBranchResult
        {
            Id = branch.Id,
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _mapper.Map<CreateBranchResult>(branch).Returns(result);

        _branchRepository.GetByNameAsync(command.Name, Arg.Any<CancellationToken>())
            .Returns((Branch?)null);
        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        _user.Username.Returns("testUser");

        // When
        var validationResponse = command.Validate();
        var createBranchResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createBranchResult.Should().NotBeNull();
        createBranchResult.Id.Should().Be(branch.Id);
        Assert.True(validationResponse.IsValid);
        Assert.Empty(validationResponse.Errors);
        await _branchRepository.Received(1).CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid branch creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid branch data When creating branch Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateBranchCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that an attempt to create a branch with an existing name throws an exception.
    /// </summary>
    [Fact(DisplayName = "Given existing branch name When creating branch Then throws invalid operation exception")]
    public async Task Handle_ExistingBranchName_ThrowsInvalidOperationException()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var existingBranch = new Branch { Name = command.Name, CreatedBy = "testUser" };

        _branchRepository.GetByNameAsync(command.Name, Arg.Any<CancellationToken>())
            .Returns(existingBranch);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Branch with name {command.Name} already exists");
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to branch entity")]
    public async Task Handle_ValidRequest_MapsCommandToBranch()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            CreatedBy = "testUser"
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _branchRepository.GetByNameAsync(command.Name, Arg.Any<CancellationToken>())
            .Returns((Branch?)null);
        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);
        _user.Username.Returns("testUser");

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Branch>(Arg.Is<CreateBranchCommand>(c =>
            c.Name == command.Name));
    }
}
