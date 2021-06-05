using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ganss.XSS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SymphTags.Domain.Entities;
using SymphTags.Persistance;
using SymphTagsApp.Application.Exceptions;
using SymphTagsApp.Application.Interfaces;

namespace SymphTagsApp.Application.Services.Links.Commands.CreateLink
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, int>
    {
        private readonly Context _context;
        private readonly ICurrentUser _currentUser;

        public CreateLinkCommandHandler(Context context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            var urlForUserExists = await _context.Link
                .AnyAsync(x => x.UserIdCreated == _currentUser.Id
                               && x.Url.Equals(request.Url)
                    , cancellationToken);

            if (urlForUserExists)
            {
                throw new DomainErrorException("This url already exists for this user.");
            }

            var link = new Link
            {
                Url = request.Url,
                UserIdCreated = _currentUser.Id
            };

            //sanitize input tags
            var sanitizer = new HtmlSanitizer();
            for (int i = 0; i < request.Tags.Count; i++)
            {
                request.Tags[i] = sanitizer.Sanitize(request.Tags[i]);
            }

            //HINT: Tags are stored in separate table
            //Idea is when user sends link with tags that we first check if that tag already exists
            //in database. If not, add it and connect ti to link, if it is already there just connect it

            //search for existing tags
            var tags = await _context.Tag
                .Where(x => request.Tags.Any(y => y.Equals(x.Name)))
                .ToListAsync(cancellationToken);

            //if new tag are sent add them to tags list
            var nonExistingTags = request.Tags.Where(x => tags.All(y => !y.Name.Equals(x)))
                .Select(x => new Tag
                {
                    Name = x
                }).ToList();

            tags.AddRange(nonExistingTags);

            //connect tags to link
            link.LinkTags = tags.Select(x => new LinkTags
            {
                Tag = x
            }).ToList();
            
            var entity = await _context.Link.AddAsync(link, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Entity.Id;
        }

    }
}
