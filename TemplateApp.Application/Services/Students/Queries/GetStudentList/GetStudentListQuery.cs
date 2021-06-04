using System.Collections.Generic;
using MediatR;

namespace TemplateApp.Application.Services.Students.Queries.GetStudentList
{
    public class GetStudentListQuery : IRequest<IList<StudentListModel>>
    {
    }
}
