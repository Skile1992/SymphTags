using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Application.Exceptions;
using TemplateApp.Domain.Entities;
using TemplateApp.Persistance;

namespace TemplateApp.Application.Services.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, int>
    {
        private readonly Context _context;

        public UpdateStudentCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Student
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (student == null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }

            student.Name = request.Name;
            student.Surname = request.Surname;
            student.CountryId = request.CountryId;
            
            await _context.SaveChangesAsync(cancellationToken);

            return student.Id;
        }
    }
}
