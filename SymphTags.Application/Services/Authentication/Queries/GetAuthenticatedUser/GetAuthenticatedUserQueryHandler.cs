using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SymphTags.Persistance;
using SymphTagsApp.Application.Infrastructure.Authorization;

namespace SymphTagsApp.Application.Services.Authentication.Queries.GetAuthenticatedUser
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
                .Where(x => x.Email.Equals(request.Email))
                .Select(x => new
                {
                    x.Id,
                    x.DisplayName,
                    x.PasswordHash,
                    x.Salt
                })
                .FirstOrDefaultAsync(cancellationToken);

            if(user == null)
                return null;

            var correctPassword = PasswordHash.VerifyPasswordHash(request.Password, user.PasswordHash, user.Salt);

            if (!correctPassword)
                return null;

            return new AuthenticatedUserModel
            {
                Id = user.Id,
                DisplayName = user.DisplayName
            };
        }

    }
}
