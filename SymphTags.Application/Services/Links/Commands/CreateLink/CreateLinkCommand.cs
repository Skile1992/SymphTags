using System.Collections.Generic;
using MediatR;

namespace SymphTagsApp.Application.Services.Links.Commands.CreateLink
{
    public class CreateLinkCommand : IRequest<int>
    {
        public string Url { get; set; }
        public List<string> Tags { get; set; }
    }
}
