using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TemplateApp.Application.Services.Authentication.Queries.GetAuthenticatedUser;

namespace TemplateApp.Controllers
{
    [ApiController]
    [Route("api/Authentication/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;

        public AuthenticationController(IMediator mediator, IOptions<AppSettings> appSettings)
        {
            _mediator = mediator;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] GetAuthenticatedUserQuery query)
        {
            var authenticatedUser = await _mediator.Send(query);

            if (authenticatedUser == null)
                return BadRequest("Invalid username or password.");
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", authenticatedUser.Id.ToString()),
                    new Claim("Username", authenticatedUser.Username),
                    new Claim("TokenIssueDate", DateTime.Now.ToString(CultureInfo.InvariantCulture))
                }),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(token)
            });
        }
    }
}
