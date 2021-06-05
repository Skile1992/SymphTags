using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SymphTagsApp.Application.Services.Authentication.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            //password between 8 and 15 characters, must have uppercase, lowercase and number
            RuleFor(x => x.Password).NotEmpty().Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$");
        }
    }
}
