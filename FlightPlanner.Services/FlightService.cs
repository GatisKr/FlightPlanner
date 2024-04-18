using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        private static readonly object _lock = new();

        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight? GetFullFlightById(int id)
        {
            return _context.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .SingleOrDefault(flight => flight.Id == id);
        }

        public void AddFlight(Flight flight)
        {
            lock (_lock)
            {
                if (FlightExists(flight))
                {
                    return;
                }

                _context.Flights.Add(flight);
                _context.SaveChanges();
            }
        }

        public bool FlightExists(Flight flight)
        {
            return _context.Flights.Any(item =>
                item.From.AirportCode == flight.From.AirportCode &&
                item.To.AirportCode == flight.To.AirportCode &&
                item.Carrier == flight.Carrier &&
                item.ArrivalTime == flight.ArrivalTime &&
                item.DepartureTime == flight.DepartureTime
            );
        }

        public List<Airport> SearchAirport(string search)
        {
            var phrase = search.Trim().ToLower();

            var airports = _context.Airports
                .Where(airport =>
                    airport.AirportCode.ToLower().Contains(phrase) ||
                    airport.City.ToLower().Contains(phrase) ||
                    airport.Country.ToLower().Contains(phrase))
                .ToList();

            airports = airports.GroupBy(a => new { a.AirportCode, a.City, a.Country })
                .Select(g => g.First())
                .ToList();

            return airports;
        }

        public PageResult SearchFlights(JsonElement request)
        {
            var page = new PageResult()
            {
                Page = 0,
                TotalItems = 0,
                Items = new List<Flight>()
            };
            var fields = GetRequestFields(request);

            var filteredFlights = _context.Flights.Where(flight =>
                flight.From.AirportCode == fields[0] &&
                flight.To.AirportCode == fields[1] &&
                flight.DepartureTime.Substring(0, 10) == fields[2]);

            page.Items.AddRange(filteredFlights);
            page.TotalItems = page.Items.Count;
            page.Page = page.TotalItems > 0 ? 1 : 0;

            return page;
        }

        public List<string> GetRequestFields(JsonElement request)
        {
            string from = "", to = "", departureDate = "";

            if (request.TryGetProperty("from", out JsonElement fromElement))
            {
                from = fromElement.GetString();
            }
            if (request.TryGetProperty("to", out JsonElement toElement))
            {
                to = toElement.GetString();
            }
            if (request.TryGetProperty("departureDate", out JsonElement departureDateElement))
            {
                departureDate = departureDateElement.GetString();
            }

            return new List<string> { from, to, departureDate };
        }

        public bool IsRequestValid(JsonElement request)
        {
            return IsNullFieldsInRequest(request) ||
                IsAirportsFromToEqual(request);
        }

        public bool IsNullFieldsInRequest(JsonElement request)
        {
            var fromProperty = request.GetProperty("from");
            var toProperty = request.GetProperty("to");
            var departureDateProperty = request.GetProperty("departureDate");

            return fromProperty.ValueKind == JsonValueKind.Null ||
                    toProperty.ValueKind == JsonValueKind.Null ||
                    departureDateProperty.ValueKind == JsonValueKind.Null;
        }

        public bool IsAirportsFromToEqual(JsonElement request)
        {
            var fields = GetRequestFields(request);

            return fields[0] == fields[1];
        }

        public bool FlightIdExists(int id)
        {
            return _context.Flights.Any(item => item.Id == id);
        }
    }
}
