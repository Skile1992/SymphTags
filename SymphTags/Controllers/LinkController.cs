using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SymphTagsApp.Application.Services.Links.Commands.CreateLink;
using SymphTagsApp.Application.Services.Links.Commands.DeleteLink;
using SymphTagsApp.Application.Services.Links.Queries.GetLinkByTagsList;
using SymphTagsApp.Application.Services.Links.Queries.GetLinkList;

namespace SymphTags.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Link/[action]")]
    public class LinkController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LinkController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateLinkCommand command)
        {
            var id = await _mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLinkCommand { Id = id });

            return NoContent();
        }

        //because is list can potently return large data, so get might not be sufficient
        [HttpPost]
        public async Task<ActionResult<IList<LinkListModel>>> List([FromBody] GetLinkListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<IList<LinkListModel>>> ListByTags([FromBody] GetLinkListByTagsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
