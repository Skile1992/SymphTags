using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SymphTagsApp.Application.Services.Tags.Commands.CreateLinkTags;
using SymphTagsApp.Application.Services.Tags.Queries.GetSuggestedTagForUrlList;
using SymphTagsApp.Application.Services.Tags.Queries.GetSuggestedTagFromOtherUsersList;

namespace SymphTags.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Tag/[action]")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddTags([FromBody] CreateLinkTagsCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
        
        //i am not sure that i have understood this task completely. By specification it says for existing tag
        //but on create link it says that it must have at least 1 tag. So this api could work with tag that is already
        //in the database but then the user had to add some custom tag when creating that link. Other option is to
        //first call this and get suggestion for url that is not in db and then create that link with tags that he/she
        //chose. I decided on the later one.
        [HttpGet]
        public async Task<ActionResult<IList<SuggestedTagListModel>>> SuggestedTagsList(string url)
        {
            var query = new GetSuggestedTagForUrlListQuery(){ Url = url };

            return Ok(await _mediator.Send(query));
        }

        //this task i didn't quite understood. If url are available only for those who have created them
        //then getting suggestion for the same link from other users don't have much sense to me
        //i have implemented in a way that will return tags for every link that is the same as this one passed by user
        [HttpGet]
        public async Task<ActionResult<IList<SuggestedTagListModel>>> SuggestedTagsFromOtherUsersList(string url)
        {
            var query = new GetSuggestedTagFromOtherUsersListQuery() { Url = url };

            return Ok(await _mediator.Send(query));
        }
    }
}
