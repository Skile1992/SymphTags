using MediatR;

namespace TemplateApp.Application.Services.Countries.Commands.CreateCountry
{
    public class CreateCountryCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
