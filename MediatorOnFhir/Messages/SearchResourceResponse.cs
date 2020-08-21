using Hl7.Fhir.Model;

namespace MediatorOnFhir.Messages
{
    public class SearchResourceResponse
    {
        public Resource Resource { get; set;  }
        public int StatusCode { get; set; }

        public static SearchResourceResponse CreateInstance(Resource resource, int statusCode)
        {
            return new SearchResourceResponse
            {
                Resource = resource,
                StatusCode = statusCode
            };
        }
    }
}