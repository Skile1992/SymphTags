using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SymphTagsApp.Application.Services.Tags.Queries.GetSuggestedTagForUrlList
{
    public class GetSuggestedTagForUrlListQueryValidator : AbstractValidator<GetSuggestedTagForUrlListQuery>
    {
        public GetSuggestedTagForUrlListQueryValidator()
        {
            //rule for url
            RuleFor(x => x.Url).NotEmpty().Must(x => Uri.IsWellFormedUriString(x, UriKind.RelativeOrAbsolute));
        }
    }
}
