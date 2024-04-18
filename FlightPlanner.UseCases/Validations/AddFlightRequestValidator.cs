using FlightPlanner.UseCases.Models;
using FluentValidation;
using System.Resources;

namespace FlightPlanner.UseCases.Validations
{
    public class AddFlightRequestValidator : AbstractValidator<AddFlightRequest>
    {
        private static readonly ResourceManager _resourceManager = new(
            "FlightPlanner.UseCases.Validations.ValidationMessages", 
            typeof(AddFlightRequestValidator).Assembly);

        public AddFlightRequestValidator()
        {
            RuleFor(request => request.Carrier)
                .NotEmpty();

            RuleFor(request => request.ArrivalTime)
                .NotEmpty();

            RuleFor(request => request.DepartureTime)
                .NotEmpty();

            RuleFor(request => request.To)
                .SetValidator(new AirportViewModelValidator());

            RuleFor(request => request.From)
                .SetValidator(new AirportViewModelValidator());

            RuleFor(request => request)
                .Must(ValidationMethods.IsDepartureArrivalDateValid)
                .WithMessage(_resourceManager.GetString("DateValid"));

            RuleFor(request => request)
                .Must(ValidationMethods.IsDepartureArrivalAirportsEqual)
                .WithMessage(_resourceManager.GetString("AirportsEqual"));
        }
    }
}
