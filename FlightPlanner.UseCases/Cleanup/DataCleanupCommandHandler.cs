using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Cleanup
{
    public class DataCleanupCommandHandler : IRequestHandler<DataCleanupCommand, ServiceResult>
    {
        private readonly IDbService _dbService;

        public DataCleanupCommandHandler(IDbService dbService)
        {
            _dbService = dbService;
        }

        public Task<ServiceResult> Handle(DataCleanupCommand request, CancellationToken cancellationToken)
        {
            _dbService.DeleteAll<Flight>();
            _dbService.DeleteAll<Airport>();

            return Task.FromResult(new ServiceResult());
        }
    }
}
