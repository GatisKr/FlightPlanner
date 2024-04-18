using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using FluentValidation;
using MediatR;
using System.Net;

namespace FlightPlanner.UseCases.Flights.AddFlight
{
    public class AddFlightCommandHandler : IRequestHandler<AddFlightCommand, ServiceResult>
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddFlightRequest> _validator;

        public AddFlightCommandHandler(
            IFlightService flightService, 
            IMapper mapper, 
            IValidator<AddFlightRequest> validator)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ServiceResult> Handle(AddFlightCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.AddFlightRequest);
            var flight = _mapper.Map<Flight>(request.AddFlightRequest);

            if (!validationResult.IsValid)
            {
                return new ServiceResult
                {
                    ResultObject = validationResult.Errors,
                    Status = HttpStatusCode.BadRequest
                };
            }
            if (_flightService.FlightExists(flight))
            {
                return new ServiceResult
                {
                    ResultObject = validationResult.Errors,
                    Status = HttpStatusCode.Conflict
                };
            }

            _flightService.AddFlight(flight);

            return new ServiceResult
            {
                ResultObject = _mapper.Map<AddFlightResponse>(flight),
                Status = HttpStatusCode.Created
            };
        }
    }
}
