using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace TemplateApp.Application.Services.Students.Queries.GetStudentDetails
{
    public class GetStudentDetailsQuery : IRequest<StudentDetailsModel>
    {
        public int Id { get; set; }
    }
}
