using System.Collections.Generic;
using MediatR;

namespace SymphTagsApp.Application.Services.Tags.Queries.GetSuggestedTagFromOtherUsersList
{
    public class GetSuggestedTagFromOtherUsersListQuery : IRequest<IList<SuggestedTagFromOtherUsersListModel>>
    {
        public string Url { get; set; }
    }
}
