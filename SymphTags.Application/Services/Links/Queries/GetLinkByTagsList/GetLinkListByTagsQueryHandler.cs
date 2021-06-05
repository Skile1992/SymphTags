using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SymphTags.Persistance;
using SymphTagsApp.Application.Interfaces;

namespace SymphTagsApp.Application.Services.Links.Queries.GetLinkByTagsList
{
    public class GetLinkListByTagsQueryHandler : IRequestHandler<GetLinkListByTagsQuery, IList<LinkListByTagsModel>>
    {
        private readonly Context _context;
        private readonly ICurrentUser _currentUser;

        public GetLinkListByTagsQueryHandler(Context context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<IList<LinkListByTagsModel>> Handle(GetLinkListByTagsQuery request, CancellationToken cancellationToken)
        {
            //returns if link has at least tag that is passed to api
            var linkByTagsModelQueryable = _context.Tag
                .Where(x => request.Tags.Any(y => y.Equals(x.Name)))
                .SelectMany(x => x.LinkTags.Select(y => y.Link))
                .Where(x => x.UserIdCreated == _currentUser.Id)
                .Select(x => new LinkListByTagsModel
                {
                    Id = x.Id,
                    Url = x.Url,
                    Tags = x.LinkTags.Select(y => new TagModel
                    {
                        Id = y.Tag.Id,
                        Name = y.Tag.Name
                    }).ToList()
                });

            //use take/skip if passed (table will have potentially large amounts of data)
            if (request.Skip.HasValue)
            {
                linkByTagsModelQueryable = linkByTagsModelQueryable.Skip(request.Skip.Value);
            }
            if (request.Take.HasValue)
            {
                linkByTagsModelQueryable = linkByTagsModelQueryable.Take(request.Take.Value);
            }

            return await linkByTagsModelQueryable
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
