using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Authentication.Common;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login
{
        public record LoginQuery(
        string email,
        string password) : IRequest<AuthenticationResult>;
}