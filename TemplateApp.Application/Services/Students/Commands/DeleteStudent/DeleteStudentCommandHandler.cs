using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Application.Exceptions;
using TemplateApp.Domain.Entities;
using TemplateApp.Persistance;

namespace TemplateApp.Application.Services.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Unit>
    {
        private readonly Context _context;

        public DeleteStudentCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Student
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (student == null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }

            _context.Student.Remove(student);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception e)
            {
                throw new DomainErrorException("This entity can not be deleted.");
            }

            return Unit.Value;
        }
    }
}
