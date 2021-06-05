using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ganss.XSS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SymphTags.Domain.Entities;
using SymphTags.Persistance;
using SymphTagsApp.Application.Exceptions;
using SymphTagsApp.Application.Interfaces;
using SymphTagsApp.Application.Services.Links.Commands.CreateLink;

namespace SymphTagsApp.Application.Services.Tags.Commands.CreateLinkTags
{
    public class CreateLinkTagsCommandHandler : IRequestHandler<CreateLinkTagsCommand, Unit>
    {
        private readonly Context _context;
        private readonly ICurrentUser _currentUser;

        public CreateLinkTagsCommandHandler(Context context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(CreateLinkTagsCommand request, CancellationToken cancellationToken)
        {
            var link = await _context.Link
                .AsNoTracking()
                .Include(x => x.LinkTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Id == request.LinkId, cancellationToken);

            if (link == null)
            {
                throw new NotFoundException(nameof(Link), request.LinkId);
            }

            if (link.UserIdCreated != _currentUser.Id)
            {
                throw new DomainErrorException("User can add tags only to links that he/she has created.");
            }

            //sanitize input tags
            var sanitizer = new HtmlSanitizer();
            for (int i = 0; i < request.Tags.Count; i++)
            {
                request.Tags[i] = sanitizer.Sanitize(request.Tags[i]);
            }

            //remove tags if are already connected to this link
            request.Tags.RemoveAll(x => link.LinkTags.Any(y => y.Tag.Name.Equals(x)));

            //search for existing tags that are in database but not connected to link
            var tags = await _context.Tag
                .Where(x => request.Tags.Any(y => y.Equals(x.Name)))
                .ToListAsync(cancellationToken);

            //if new tag are sent add them to
            var nonExistingTags = request.Tags.Where(x => tags.All(y => !y.Name.Equals(x)))
                .Select(x => new Tag
                {
                    Name = x
                }).ToList();

            tags.AddRange(nonExistingTags);

            //add tags
            var linkTags = tags.Select(x => new LinkTags
            {
                LinkId = link.Id,
                Tag = x
            }).ToList();

            await _context.LinkTags.AddRangeAsync(linkTags, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
