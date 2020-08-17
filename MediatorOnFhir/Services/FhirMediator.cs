using System.Threading.Tasks;
using MediatorOnFhir.Messages;
using MediatR;

namespace MediatorOnFhir.Services
{
    /// <summary>
    /// Simple example of sub-classing the mediator.
    /// </summary>
    public class FhirMediator : Mediator
    {
        public FhirMediator(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        public async Task<SearchResourceResponse> SendSearchResourceRequest(SearchResourceRequest searchResourceRequest)
        {
            return await Send(searchResourceRequest);
        }
    }

    public interface IFhirMediatorService
    {
        Task<SearchResourceResponse> SendSearchResourceRequest(SearchResourceRequest searchResourceRequest);
    }

    /// <summary>
    /// Service Implementation with interface
    /// </summary>
    public class FhirMediatorService : IFhirMediatorService
    {
        private readonly IMediator _mediator;
        public FhirMediatorService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<SearchResourceResponse> SendSearchResourceRequest(SearchResourceRequest searchResourceRequest)
        {
            return _mediator.Send(searchResourceRequest);
        }
    }
}