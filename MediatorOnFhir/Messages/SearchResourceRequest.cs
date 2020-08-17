using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using MediatR;

namespace MediatorOnFhir.Messages
{
    public class SearchResourceRequest : IRequest<SearchResourceResponse>
    {
        public SearchParams SearchParams { get; }
        public string ResourceType { get; }

        public static SearchResourceRequest CreateInstance(SearchParams searchParams, string resourceType)
        {
            return new SearchResourceRequest(searchParams, resourceType);
        }

        private SearchResourceRequest(SearchParams searchParams, string resourceType)
        {
            SearchParams = searchParams;
            ResourceType = resourceType;
        }
    }
}