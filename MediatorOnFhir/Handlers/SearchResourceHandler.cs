using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;
using MediatorOnFhir.Messages;
using MediatR;
using Microsoft.Extensions.Logging;
using static MediatorOnFhir.Extensions.FhirClientExtensions;

namespace MediatorOnFhir.Handlers
{
    public class SearchResourceHandler : IRequestHandler<SearchResourceRequest, SearchResourceResponse>
    {
        private readonly IFhirClient _fhirClient;
        private readonly ILogger<SearchResourceHandler> _logger;

        public SearchResourceHandler(IFhirClient fhirClient, ILogger<SearchResourceHandler> logger)
        {
            _fhirClient = fhirClient;
            _logger = logger;
        }

        public async Task<SearchResourceResponse> Handle(SearchResourceRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Performing Search Request...");
            var resource = await _fhirClient.PerformSearch(request.SearchParams, request.ResourceType);
            return SearchResourceResponse.CreateInstance(resource, 200);
        }
    }
}