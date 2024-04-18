using FlightPlanner.UseCases.Flights.GetFlight;
using FlightPlanner.UseCases.Flights.SearchFlights;
using FlightPlanner.UseCases.Search.SearchAirports;
using FlightPlanner.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FlightPlanner.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("airports")]
        public async Task<IActionResult> SearchAirports(string search)
        {
            return (await _mediator
                .Send(new SearchAirportsQuery(search)))
                .ToActionResult();
        }

        [HttpPost]
        [Route("flights/search")]
        public async Task<IActionResult> SearchFlights(JsonElement request)
        {
            return (await _mediator
                .Send(new SearchFlightsQuery(request)))
                .ToActionResult();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> FindFlightById(int id)
        {
            return (await _mediator
                .Send(new GetFlightQuery(id)))
                .ToActionResult();
        }
    }
}
