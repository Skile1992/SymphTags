using System.Collections.Generic;
using MediatR;

namespace SymphTagsApp.Application.Services.Tags.Commands.CreateLinkTags
{
    public class CreateLinkTagsCommand : IRequest
    {
        public int LinkId { get; set; }
        public List<string> Tags { get; set; }
    }
}
