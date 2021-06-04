using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Application.Exceptions;
using TemplateApp.Application.Services.Students.Queries.GetStudentDetails;
using TemplateApp.Domain.Entities;
using TemplateApp.Persistance;

namespace TemplateApp.Application.Services.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly Context _context;

        public CreateStudentCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                Name = request.Name,
                Surname = request.Surname,
                CountryId = request.CountryId
            };

            var entity = await _context.Student.AddAsync(student, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Entity.Id;
        }
    }
}
