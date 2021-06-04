using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace TemplateApp.Application.Services.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CountryId { get; set; }
    }
}
