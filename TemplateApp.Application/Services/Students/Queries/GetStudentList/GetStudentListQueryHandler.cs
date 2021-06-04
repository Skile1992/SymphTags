using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Application.Exceptions;
using TemplateApp.Persistance;

namespace TemplateApp.Application.Services.Students.Queries.GetStudentList
{
    public class GetStudentListQueryHandler : IRequestHandler<GetStudentListQuery, IList<StudentListModel>>
    {
        private readonly Context _context;

        public GetStudentListQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<IList<StudentListModel>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentListModel = await _context.Student
                .Select(x => new StudentListModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    CountryName = x.Country.Name
                })
                .ToListAsync(cancellationToken);
            
            return studentListModel;
        }

    }
}
