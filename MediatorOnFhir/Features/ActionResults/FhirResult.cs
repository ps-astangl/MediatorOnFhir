using EnsureThat;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;

namespace MediatorOnFhir.Features.ActionResults
{
    public class FhirResult : ObjectResult
    {
        public static FhirResult CreateInstance(Resource resource, int statusCode)
        {
            EnsureArg.IsNotNull(resource, nameof(resource));
            return new FhirResult(resource, statusCode);
        }

        private FhirResult(Resource resource, int statusCode) : base(resource)
        {
            StatusCode = statusCode;
        }
    }
}