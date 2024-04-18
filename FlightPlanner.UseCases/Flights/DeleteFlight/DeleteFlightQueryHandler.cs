using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.DeleteFlight
{
    public class DeleteFlightQueryHandler : IRequestHandler<DeleteFlightQuery, ServiceResult>
    {
        private readonly IFlightService _flightService;

        public DeleteFlightQueryHandler(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public async Task<ServiceResult> Handle(DeleteFlightQuery request, CancellationToken cancellationToken)
        {
            var flight = _flightService.GetFullFlightById(request.Id);
            var response = new ServiceResult();

            if (flight != null)
            {
                _flightService.Delete(flight);
            }

            return response;
        }
    }
}
