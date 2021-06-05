using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SymphTagsApp.Application.Exceptions;
using SymphTagsApp.Application.Interfaces;
using SymphTagsApp.Application.Services.Tags.BL;

namespace SymphTagsApp.Application.Services.Tags.Queries.GetSuggestedTagForUrlList
{
    public class GetSuggestedTagForUrlListQueryHandler : IRequestHandler<GetSuggestedTagForUrlListQuery, IList<SuggestedTagListModel>>
    {
        private readonly IWebsiteParser _websiteParser;

        public GetSuggestedTagForUrlListQueryHandler(IWebsiteParser websiteParser)
        {
            _websiteParser = websiteParser;
        }

        public async Task<IList<SuggestedTagListModel>> Handle(GetSuggestedTagForUrlListQuery request, CancellationToken cancellationToken)
        {
            List<string> tags;
            try
            {
                tags = await _websiteParser.Parse(request.Url);
            }
            catch
            {
                throw new DomainErrorException("Can not parse requested webpage.");
            }
            
            tags = TagsCleaner.Sanitize(tags);

            var suggestedTags = tags
                .GroupBy(x => x.ToLower())
                .Select(x => new SuggestedTagListModel
                {
                    Tag = x.Key,
                    NumberOfOccurence = x.Count()
                })
                //sort and take only first 10 where number of occurence is greater than 2
                .Where(x => x.NumberOfOccurence > 2)
                .OrderByDescending(x => x.NumberOfOccurence)
                .Take(10)
                .ToList();

            //by specification it says that must have at least 1 element
            //i could throw exception here but is more natural to me to just return empty list
            return suggestedTags;
        }
    }
}
