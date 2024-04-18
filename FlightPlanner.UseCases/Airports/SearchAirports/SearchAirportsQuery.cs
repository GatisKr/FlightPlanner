using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Search.SearchAirports
{
    public class SearchAirportsQuery(string search) : IRequest<ServiceResult>
    {
        public string Search { get; } = search;
    }
}
