using System;
using FluentValidation;

namespace SymphTagsApp.Application.Services.Tags.Queries.GetSuggestedTagFromOtherUsersList
{
    public class GetSuggestedTagFromOtherUsersListQueryValidator : AbstractValidator<GetSuggestedTagFromOtherUsersListQuery>
    {
        public GetSuggestedTagFromOtherUsersListQueryValidator()
        {
            //rule for url
            RuleFor(x => x.Url).NotEmpty().Must(x => Uri.IsWellFormedUriString(x, UriKind.RelativeOrAbsolute));
        }
    }
}
