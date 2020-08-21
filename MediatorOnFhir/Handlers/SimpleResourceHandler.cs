using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using MediatorOnFhir.Extensions;
using MediatorOnFhir.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorOnFhir.Handlers
{
    public class SimpleResourceHandler : IRequestHandler<SimpleResourceRequest, Resource>
    {
        private readonly IFhirClient _fhirClient;
        private readonly ILogger<SimpleResourceHandler> _logger;

        public SimpleResourceHandler(IFhirClient fhirClient, ILogger<SimpleResourceHandler> logger)
        {
            _fhirClient = fhirClient;
            _logger = logger;
        }

        public async Task<Resource> Handle(SimpleResourceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Performing simple resource search...");
                return await _fhirClient.PerformSearch(request.SearchParams, request.ResourceType);
            }
            catch (FhirOperationException fhirOperationException)
            {
                return fhirOperationException.Outcome;
            }
        }
    }
}