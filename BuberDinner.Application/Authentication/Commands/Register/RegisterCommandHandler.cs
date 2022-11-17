using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Common.Interfaces.Authentication;

using BuberDinner.Domain.Entities;
using MediatR;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Errors;

namespace BuberDinner.Application.Commands.Register
{

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, 
                                    IUserRepository userRepository )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }


        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            //1- Validate the user doesn't exist
            if (_userRepository.GetUserByEmail(command.email) is not null)
            {
                throw new DuplicateEmailException();
            }

            // 2- Create user (generate unique id) & Persist to DB
            var user = new User
            {
                FirstName = command.firstName,
                LastName = command.lastName,
                Email = command.email,
                Password = command.password
            };

            _userRepository.Add(user);

            //3- Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}