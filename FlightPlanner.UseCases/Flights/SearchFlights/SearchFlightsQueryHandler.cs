using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;
using System.Net;

namespace FlightPlanner.UseCases.Flights.SearchFlights
{
    public class SearchFlightsQueryHandler : IRequestHandler<SearchFlightsQuery, ServiceResult>
    {
        private readonly IFlightService _flightService;

        public SearchFlightsQueryHandler(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public async Task<ServiceResult> Handle(SearchFlightsQuery request, CancellationToken cancellationToken)
        {
            if (_flightService.IsRequestValid(request.Request))
            {
                return new ServiceResult
                {
                    Status = HttpStatusCode.BadRequest
                };
            }

            var flights = _flightService.SearchFlights(request.Request);

            return new ServiceResult
            {
                ResultObject = flights,
                Status = HttpStatusCode.OK
            };
        }
    }
}
