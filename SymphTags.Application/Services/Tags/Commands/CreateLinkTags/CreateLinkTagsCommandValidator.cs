using FluentValidation;

namespace SymphTagsApp.Application.Services.Tags.Commands.CreateLinkTags
{
    public class CreateLinkTagsCommandValidator : AbstractValidator<CreateLinkTagsCommand>
    {
        public CreateLinkTagsCommandValidator()
        {
            RuleFor(x => x.LinkId).NotEmpty();
            //at least one tag
            RuleFor(x => x.Tags).Must(x => x.Count > 0);
        }
    }
}
