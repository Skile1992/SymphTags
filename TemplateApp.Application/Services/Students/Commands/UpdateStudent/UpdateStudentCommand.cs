using MediatR;

namespace TemplateApp.Application.Services.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CountryId { get; set; }
    }
}
