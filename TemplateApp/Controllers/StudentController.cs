using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using TemplateApp.Application.Services.Students.Commands.CreateStudent;
using TemplateApp.Application.Services.Students.Commands.DeleteStudent;
using TemplateApp.Application.Services.Students.Commands.UpdateStudent;
using TemplateApp.Application.Services.Students.Queries.GetStudentDetails;
using TemplateApp.Application.Services.Students.Queries.GetStudentList;

namespace TemplateApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Student/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetailsModel>> Get(int id)
        {
            return Ok(await _mediator.Send(new GetStudentDetailsQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<IList<StudentListModel>>> List()
        {
            return Ok(await _mediator.Send(new GetStudentListQuery { }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
        {
            var id = await _mediator.Send(command);

            return Ok(id);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteStudentCommand { Id = id });

            return NoContent();
        }

    }
}
