using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler: IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, 
                                    IUserRepository userRepository )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }


        public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
           //1- Validate the user exists
            if (_userRepository.GetUserByEmail(query.email) is not User user)
            {
                throw new Exception("User with given email does not exist.");
            }

            //2- Validate the password is correct
            if (user.Password != query.password)
            {
                throw new Exception("Invalid password");
            }

            //3- Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user, 
                token);
        
        }
    }
}