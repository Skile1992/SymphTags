using MediatR;

namespace SymphTagsApp.Application.Services.Links.Commands.DeleteLink
{
    public class DeleteLinkCommand : IRequest
    {
        public int Id { get; set; }
    }
}
