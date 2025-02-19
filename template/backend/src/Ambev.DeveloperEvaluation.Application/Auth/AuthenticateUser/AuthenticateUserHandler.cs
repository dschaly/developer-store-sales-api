using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser
{
    /// <summary>
    /// Handler for processing AuthenticateUserCommand requests
    /// </summary>
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        /// <summary>
        /// Initializes a new instance of CreateUserHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="passwordHasher">The Password Hasher</param>
        /// <param name="jwtTokenGenerator">The JWT generator</param>
        public AuthenticateUserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        /// <summary>
        /// Handles the AuthenticateUserCommand request
        /// </summary>
        /// <param name="command">The AuthenticateUser command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The authenticated user details</returns>
        public async Task<AuthenticateUserResult> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

            if (user == null || !_passwordHasher.VerifyPassword(command.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var activeUserSpec = new ActiveUserSpecification();
            if (!activeUserSpec.IsSatisfiedBy(user))
            {
                throw new UnauthorizedAccessException("User is not active");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticateUserResult
            {
                Token = token,
                Email = user.Email,
                Name = user.Username,
                Role = user.Role.ToString()
            };
        }
    }
}
