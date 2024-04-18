using FlightPlanner.UseCases.Models;

namespace FlightPlanner.UseCases.Validations
{
    public class ValidationMethods
    {
        public static bool IsDepartureArrivalDateValid(AddFlightRequest flight)
        {
            var dateArrival = DateTime.Parse(flight.ArrivalTime);
            var dateDeparture = DateTime.Parse(flight.DepartureTime);

            return dateArrival > dateDeparture;
        }

        public static bool IsDepartureArrivalAirportsEqual(AddFlightRequest flight)
        {
            return flight.From.Airport.ToLower().Trim()
                != flight.To.Airport.ToLower().Trim();
        }
    }
}
