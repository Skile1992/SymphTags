using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using TemplateApp.Application.Services.Countries.Commands.CreateCountry;
using TemplateApp.Application.Services.Countries.Queries.GetCountryDetails;
using TemplateApp.Application.Services.Students.Commands.CreateStudent;
using TemplateApp.Application.Services.Students.Queries.GetStudentDetails;

namespace TemplateApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Country/[action]")]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDetailsModel>> Get(int id)
        {
            return Ok(await _mediator.Send(new GetCountryDetailsQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateCountryCommand command)
        {
            var id = await _mediator.Send(command);

            return Ok(id);
        }

    }
}
