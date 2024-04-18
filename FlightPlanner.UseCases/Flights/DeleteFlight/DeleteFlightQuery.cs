using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.DeleteFlight
{
    public class DeleteFlightQuery(int id) : IRequest<ServiceResult>
    {
        public int Id { get; } = id;
    }
}
