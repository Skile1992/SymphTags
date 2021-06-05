using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SymphTags.Persistance;
using SymphTagsApp.Application.Interfaces;

namespace SymphTagsApp.Application.Services.Links.Queries.GetLinkList
{
    public class GetLinkListQueryHandler : IRequestHandler<GetLinkListQuery, IList<LinkListModel>>
    {
        private readonly Context _context;
        private readonly ICurrentUser _currentUser;

        public GetLinkListQueryHandler(Context context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<IList<LinkListModel>> Handle(GetLinkListQuery request, CancellationToken cancellationToken)
        {
            var linkModelQueryable = _context.Link
                .Where(x => x.UserIdCreated == _currentUser.Id)
                .Select(x => new LinkListModel
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
            if(request.Skip.HasValue)
            {
                linkModelQueryable = linkModelQueryable.Skip(request.Skip.Value);
            }
            if (request.Take.HasValue)
            {
                linkModelQueryable = linkModelQueryable.Take(request.Take.Value);
            }

            return await linkModelQueryable
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
