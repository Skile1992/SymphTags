using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SymphTags.Domain.Entities;
using SymphTags.Persistance;
using SymphTagsApp.Application.Exceptions;

namespace SymphTagsApp.Application.Services.Authentication.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly Context _context;

        public CreateUserCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailOrUsernameExists = await _context.User
                .AnyAsync(x => x.DisplayName.Equals(request.DisplayName) || x.Email.Equals(request.Email)
                               , cancellationToken);

            if(emailOrUsernameExists)
            {
                throw new DomainErrorException("Email or username already exists.");
            }

            //for now fixed length salt
            var salt = MyUtils.GenerateRandomString(5);
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(salt));

            var user = new User
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                EmailVerified = false,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                Salt = Encoding.UTF8.GetBytes(salt)
            };

            var entity = await _context.User.AddAsync(user, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Entity.Id;
        }
    }
}
