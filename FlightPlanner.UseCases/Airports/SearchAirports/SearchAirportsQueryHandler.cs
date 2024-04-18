using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;
using System.Net;

namespace FlightPlanner.UseCases.Search.SearchAirports
{
    public class SearchAirportsQueryHandler : IRequestHandler<SearchAirportsQuery, ServiceResult>
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public SearchAirportsQueryHandler(
            IFlightService flightService,
            IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        public async Task<ServiceResult> Handle(SearchAirportsQuery request, CancellationToken cancellationToken)
        {
            var airport = _flightService.SearchAirport(request.Search);

            return new ServiceResult
            {
                ResultObject = _mapper.Map<List<AirportViewModel>>(airport),
                Status = HttpStatusCode.OK
            };
        }
    }
}
