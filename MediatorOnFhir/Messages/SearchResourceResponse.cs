using EnsureThat;
using Hl7.Fhir.Model;

namespace MediatorOnFhir.Messages
{
    public class SearchResourceResponse
    {
        public static SearchResourceResponse CreateInstance(Resource resource)
        {
            return new SearchResourceResponse(resource);
        }

        private SearchResourceResponse(Resource resource) => Resource = resource;

        public Resource Resource { get; }
    }
}