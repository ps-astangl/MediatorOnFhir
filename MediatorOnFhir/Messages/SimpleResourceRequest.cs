using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using MediatR;

namespace MediatorOnFhir.Messages
{
    public class SimpleResourceRequest : BaseResourceRequest, IRequest<Resource>
    {
        public static SimpleResourceRequest CreateInstance(SearchParams searchParams, string resourceType)
        {
            return new SimpleResourceRequest(searchParams, resourceType);
        }

        private SimpleResourceRequest(SearchParams searchParams, string resourceType)
        {
            SearchParams = searchParams;
            ResourceType = resourceType;
        }
    }
}