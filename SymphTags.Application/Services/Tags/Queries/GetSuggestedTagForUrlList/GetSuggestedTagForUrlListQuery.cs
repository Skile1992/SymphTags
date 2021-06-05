using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SymphTagsApp.Application.Services.Tags.Queries.GetSuggestedTagForUrlList
{
    public class GetSuggestedTagForUrlListQuery : IRequest<IList<SuggestedTagListModel>>
    {
        public string Url { get; set; }
    }
}
