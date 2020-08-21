using Hl7.Fhir.Rest;

namespace MediatorOnFhir.Messages
{
    public abstract class BaseResourceRequest
    {
        public SearchParams SearchParams { get; set; }
        public string ResourceType { get; set; }
    }
}