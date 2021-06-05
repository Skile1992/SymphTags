using System.Collections.Generic;
using MediatR;

namespace SymphTagsApp.Application.Services.Links.Queries.GetLinkList
{
    public class GetLinkListQuery : IRequest<IList<LinkListModel>>
    {
        //if there are potentially many data this will allow paging for user
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
}
