using System;
using FluentValidation;

namespace SymphTagsApp.Application.Services.Links.Commands.CreateLink
{
    public class CreateLinkCommandValidator : AbstractValidator<CreateLinkCommand>
    {
        public CreateLinkCommandValidator()
        {
            //rule for url
            RuleFor(x => x.Url).NotEmpty().Must(x => Uri.IsWellFormedUriString(x, UriKind.RelativeOrAbsolute));
            //under specification must have at least one tag
            RuleFor(x => x.Tags).Must(x => x.Count > 0);
        }

    }
}
