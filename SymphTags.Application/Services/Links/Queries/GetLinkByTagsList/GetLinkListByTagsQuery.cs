using System.Collections.Generic;
using MediatR;

namespace SymphTagsApp.Application.Services.Links.Queries.GetLinkByTagsList
{
    public class GetLinkListByTagsQuery : IRequest<IList<LinkListByTagsModel>>
    {
        public List<string> Tags { get; set; }
        //if there are potentially many data this will allow paging for user
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
}
