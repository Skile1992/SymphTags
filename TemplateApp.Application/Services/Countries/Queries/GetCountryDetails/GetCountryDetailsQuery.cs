using MediatR;

namespace TemplateApp.Application.Services.Countries.Queries.GetCountryDetails
{
    public class GetCountryDetailsQuery : IRequest<CountryDetailsModel>
    {
        public int Id { get; set; }
    }
}
