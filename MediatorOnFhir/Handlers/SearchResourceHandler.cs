using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using MediatorOnFhir.Messages;
using MediatR;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;

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
        
        public async Task<SearchResourceResponse> Handle(SearchResourceRequest request, CancellationToken cancellationToken)
        {
            var resource = await PerformSearch(request);
            return SearchResourceResponse.CreateInstance(resource);
        }

        private async Task<Resource> PerformSearch(SearchResourceRequest request)
        {
            var pagedResult = await Task.Run(() =>
            {
                var pagedResultTask = _fhirClient.SearchAsync(request.SearchParams, request.ResourceType);
                return pagedResultTask;
            });

            Bundle bundle = pagedResult.DeepCopy() as Bundle;
            int count = 1;
            do
            {
                _logger.LogInformation($"Paging result: {count}...");
                pagedResult = await _fhirClient.ContinueAsync(pagedResult);
                if (pagedResult?.Entry == null) continue;
                bundle?.Entry?.AddRange(pagedResult.Entry);
                count++;
            } while (pagedResult != null);

            return bundle;
        }
    }
}