using MediatR;

namespace SymphTagsApp.Application.Services.Authentication.Queries.GetAuthenticatedUser
{
    public class GetAuthenticatedUserQuery : IRequest<AuthenticatedUserModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
