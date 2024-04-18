using FlightPlanner.Core.Models;
using System.Text.Json;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight? GetFullFlightById(int id);
        void AddFlight(Flight flight);
        bool FlightExists(Flight flight);
        List<Airport> SearchAirport(string search);
        bool IsRequestValid(JsonElement request);
        bool FlightIdExists(int id);
        PageResult SearchFlights(JsonElement request);
    }
}
