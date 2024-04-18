using FlightPlanner.UseCases.Models;
using MediatR;
using System.Text.Json;

namespace FlightPlanner.UseCases.Flights.SearchFlights
{
    public class SearchFlightsQuery(JsonElement request) : IRequest<ServiceResult>
    {
        public JsonElement Request { get; } = request;
    }
}
