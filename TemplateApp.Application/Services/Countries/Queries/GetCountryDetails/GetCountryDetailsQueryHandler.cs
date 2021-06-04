using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Application.Exceptions;
using TemplateApp.Persistance;

namespace TemplateApp.Application.Services.Countries.Queries.GetCountryDetails
{
    public class GetCountryDetailsQueryHandler : IRequestHandler<GetCountryDetailsQuery, CountryDetailsModel>
    {
        private readonly Context _context;

        public GetCountryDetailsQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<CountryDetailsModel> Handle(GetCountryDetailsQuery request, CancellationToken cancellationToken)
        {
            var countryDetailsModel = await _context.Country
                .Where(p => p.Id == request.Id)
                .Select(x => new CountryDetailsModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (countryDetailsModel == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Student), request.Id);
            }
            
            return countryDetailsModel;
        }

    }
}
