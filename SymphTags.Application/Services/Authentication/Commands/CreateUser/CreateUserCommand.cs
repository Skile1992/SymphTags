using MediatR;

namespace SymphTagsApp.Application.Services.Authentication.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}
