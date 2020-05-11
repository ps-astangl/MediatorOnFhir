using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using MediatorOnFhir.Messages;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace MediatorOnFhir.Handlers
{
    public class SearchResourceHandler : IRequestHandler<SearchResourceRequest, SearchResourceResponse>
    {
        private readonly IFhirClient _fhirClient;

        public SearchResourceHandler(IFhirClient fhirClient)
        {
            _fhirClient = fhirClient;
        }
        
        public async Task<SearchResourceResponse> Handle(SearchResourceRequest request, CancellationToken cancellationToken)
        {
            return new SearchResourceResponse(await PerformSearch(request));
        }

        private async Task<Resource> PerformSearch(SearchResourceRequest request)
        {
            var pagedResult = await Task.Run(() =>
            {
                var pagedResultTask = _fhirClient.SearchAsync(request.SearchParams, request.ResourceType);
                return pagedResultTask;
            });
            Bundle bundle = pagedResult.DeepCopy() as Bundle;


            while (pagedResult != null)
            {
                pagedResult = await _fhirClient.ContinueAsync(pagedResult);
                if (pagedResult?.Entry != null)
                {
                    bundle?.Entry?.AddRange(pagedResult.Entry);
                }

            }
            return bundle;
        }
    }
}