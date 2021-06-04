using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Application.Exceptions;
using TemplateApp.Persistance;

namespace TemplateApp.Application.Services.Students.Queries.GetStudentDetails
{
    public class GetStudentDetailsQueryHandler : IRequestHandler<GetStudentDetailsQuery, StudentDetailsModel>
    {
        private readonly Context _context;

        public GetStudentDetailsQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<StudentDetailsModel> Handle(GetStudentDetailsQuery request, CancellationToken cancellationToken)
        {
            var studentDetailsModel = await _context.Student
                .Where(p => p.Id == request.Id)
                .Select(x => new StudentDetailsModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    CountryName = x.Country.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (studentDetailsModel == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Student), request.Id);
            }

            return studentDetailsModel;
        }

    }
}
