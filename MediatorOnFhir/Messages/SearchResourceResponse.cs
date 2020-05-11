using System.Threading.Tasks;
using EnsureThat;
using Hl7.Fhir.Model;

namespace MediatorOnFhir.Messages
{
    public class SearchResourceResponse
    {
        public SearchResourceResponse(Resource resource)
        {
            EnsureArg.IsNotNull(resource, nameof(resource));
            Resource = resource;
        }

        public Resource Resource { get; }
    }
}