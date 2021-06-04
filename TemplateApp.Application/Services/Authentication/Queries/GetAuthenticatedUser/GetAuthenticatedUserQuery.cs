using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace TemplateApp.Application.Services.Authentication.Queries.GetAuthenticatedUser
{
    public class GetAuthenticatedUserQuery : IRequest<AuthenticatedUserModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
