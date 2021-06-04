using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Application.Exceptions;
using TemplateApp.Application.Infrastructure.Authorization;
using TemplateApp.Application.Services.Countries.Queries.GetCountryDetails;
using TemplateApp.Persistance;

namespace TemplateApp.Application.Services.Authentication.Queries.GetAuthenticatedUser
{
    public class GetAuthenticatedUserQueryHandler : IRequestHandler<GetAuthenticatedUserQuery, AuthenticatedUserModel>
    {
        private readonly Context _context;

        public GetAuthenticatedUserQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<AuthenticatedUserModel> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.User
                .Where(x => x.Username.Equals(request.Username))
                .Select(x => new
                {
                    Id = x.Id,
                    Username = x.Username,
                    PasswordHash = x.PasswordHash,
                    Salt = x.Salt
                })
                .FirstOrDefaultAsync(cancellationToken);

            if(user == null)
                return null;

            var hmac = new HMACSHA512(user.Salt);
            var test = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin"));

            var correctPassword = PasswordHash.VerifyPasswordHash(request.Password, user.PasswordHash, user.Salt);

            if (!correctPassword)
                return null;

            return new AuthenticatedUserModel
            {
                Id = user.Id,
                Username = user.Username
            };
        }

    }
}
