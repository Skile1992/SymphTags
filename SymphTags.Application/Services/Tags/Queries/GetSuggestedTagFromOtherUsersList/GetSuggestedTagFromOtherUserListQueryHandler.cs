using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SymphTags.Persistance;

namespace SymphTagsApp.Application.Services.Tags.Queries.GetSuggestedTagFromOtherUsersList
{
    public class GetSuggestedTagFromOtherUsersListQueryHandler : IRequestHandler<GetSuggestedTagFromOtherUsersListQuery, IList<SuggestedTagFromOtherUsersListModel>>
    {
        private readonly Context _context;

        public GetSuggestedTagFromOtherUsersListQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<IList<SuggestedTagFromOtherUsersListModel>> Handle(GetSuggestedTagFromOtherUsersListQuery request, CancellationToken cancellationToken)
        {
            //returns tags that are connected to link that is passed
            var suggestedTags = await _context.LinkTags
                .Where(x => x.Link.Url.Equals(request.Url))
                .GroupBy(x => new
                {
                    x.Tag.Id,
                    x.Tag.Name
                })
                .Select(x => new SuggestedTagFromOtherUsersListModel
                {
                    Tag = x.Key.Name,
                    NumberOfOccurence = x.Count()
                })
                //sort and take only first 10
                .OrderByDescending(x => x.NumberOfOccurence)
                .Take(10)
                .ToListAsync(cancellationToken);

            return suggestedTags;
        }
    }
}
