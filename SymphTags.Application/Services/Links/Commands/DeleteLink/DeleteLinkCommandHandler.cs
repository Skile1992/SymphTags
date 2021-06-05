using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SymphTags.Domain.Entities;
using SymphTags.Persistance;
using SymphTagsApp.Application.Exceptions;
using SymphTagsApp.Application.Interfaces;

namespace SymphTagsApp.Application.Services.Links.Commands.DeleteLink
{
    public class DeleteLinkCommandHandler : IRequestHandler<DeleteLinkCommand, Unit>
    {
        private readonly Context _context;
        private readonly ICurrentUser _currentUser;

        public DeleteLinkCommandHandler(Context context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(DeleteLinkCommand request, CancellationToken cancellationToken)
        {
            var link = await _context.Link
                .Include(x => x.LinkTags)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (link == null)
            {
                throw new NotFoundException(nameof(Link), request.Id);
            }

            if(link.UserIdCreated != _currentUser.Id)
            {
                throw new DomainErrorException("User can delete only links that he/she has created.");
            }

            _context.LinkTags.RemoveRange(link.LinkTags);
            _context.Link.Remove(link);
            
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
