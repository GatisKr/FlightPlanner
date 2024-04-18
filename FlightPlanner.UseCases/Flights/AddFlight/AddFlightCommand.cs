using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.AddFlight
{
    public class AddFlightCommand : IRequest<ServiceResult>
    {
        public AddFlightRequest AddFlightRequest { get; set; }
    }
}
