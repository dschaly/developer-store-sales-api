using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class BranchTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid Branch entities.
        /// The generated branch will have valid:
        /// - Id (using random GUID)
        /// - Name (using Company names)
        /// - Address (using full address)
        /// - CreatedAt (using past dates)
        /// - CreatedBy (using internet usernames)
        /// </summary>
        private static readonly Faker<Branch> BranchFaker = new Faker<Branch>()
            .RuleFor(b => b.Id, f => f.Random.Guid())
            .RuleFor(b => b.Name, f => f.Company.CompanyName())
            .RuleFor(b => b.Address, f => f.Address.FullAddress())
            .RuleFor(b => b.CreatedAt, f => f.Date.Past())
            .RuleFor(b => b.CreatedBy, f => f.Person.FullName);

        /// <summary>
        /// Generates a valid Branch entity with randomized data.
        /// The generated branch will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid Branch entity with randomly generated data.</returns>
        public static Branch GenerateValidBranch()
        {
            return BranchFaker.Generate();
        }

        /// <summary>
        /// Generates a valid Guid Id name using Faker.
        /// The generated name will:
        /// - Follow the Guid format
        /// </summary>
        /// <returns>A valid Guid.</returns>
        public static Guid GenerateValidId()
        {
            return new Faker().Random.Guid();
        }

        /// <summary>
        /// Generates an invalid branch with missing or incorrect data.
        /// </summary>
        /// <returns>An invalid Branch entity.</returns>
        public static Branch GenerateInvalidBranch()
        {
            return new Branch
            {
                Id = Guid.Empty, // Invalid ID
                CreatedAt = default, // Invalid Date
                CreatedBy = string.Empty, // Empty CreatedBy
                Name = string.Empty, // Empty Name
                Address = string.Empty // Empty Address
            };
        }

        /// <summary>
        /// Generates a valid branch name.
        /// </summary>
        /// <returns>A valid branch name.</returns>
        public static string GenerateValidBranchName()
        {
            return new Faker().Company.CompanyName();
        }

        /// <summary>
        /// Generates a valid address.
        /// </summary>
        /// <returns>A valid address.</returns>
        public static string GenerateValidAddress()
        {
            return new Faker().Address.FullAddress();
        }
    }
}