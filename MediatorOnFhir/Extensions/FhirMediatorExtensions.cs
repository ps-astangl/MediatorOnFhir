using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;
using MediatorOnFhir.Messages;
using MediatR;

namespace MediatorOnFhir.Extensions
{
    public static class FhirMediatorExtensions
    {
        public static async Task<SearchResourceResponse> SearchResourceAsync(
            this IMediator mediator, 
            string resourceType, 
            SearchParams searchParams,
            CancellationToken cancellationToken = default)
        {
            var searchResourceRequest = SearchResourceRequest.CreateInstance(searchParams, resourceType);
            return await mediator.Send(searchResourceRequest, cancellationToken);
        }
    }
}