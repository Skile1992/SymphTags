using MediatR;

namespace TemplateApp.Application.Services.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommand : IRequest
    {
        public int Id { get; set; }
    }
}
