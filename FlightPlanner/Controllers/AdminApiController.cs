﻿using FlightPlanner.UseCases.Flights.AddFlight;
using FlightPlanner.UseCases.Flights.DeleteFlight;
using FlightPlanner.UseCases.Flights.GetFlight;
using FlightPlanner.UseCases.Models;
using FlightPlanner.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Web.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> GetFlight(int id)
        {
            return (await _mediator
                .Send(new GetFlightQuery(id)))
                .ToActionResult();
        }

        [HttpPut]
        [Route("flights")]
        public async Task<IActionResult> AddFlight(AddFlightRequest request)
        {
            return (await _mediator
                .Send(new AddFlightCommand { AddFlightRequest = request }))
                .ToActionResult();
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            return (await _mediator
                .Send(new DeleteFlightQuery(id)))
                .ToActionResult();
        }
    }
}
