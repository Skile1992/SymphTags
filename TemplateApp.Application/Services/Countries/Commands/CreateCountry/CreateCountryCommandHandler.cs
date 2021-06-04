using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TemplateApp.Domain.Entities;
using TemplateApp.Persistance;

namespace TemplateApp.Application.Services.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly Context _context;

        public CreateCountryCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = new Country
            {
                Name = request.Name
            };

            var entity = await _context.Country.AddAsync(country, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Entity.Id;
        }
    }
}
