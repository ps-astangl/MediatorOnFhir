using System;
using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using MediatorOnFhir.Extensions;
using MediatorOnFhir.Features.ActionResults;
using MediatorOnFhir.Messages;
using MediatorOnFhir.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediatorOnFhir.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FhirController : ControllerBase
    {
        private readonly ILogger<FhirController> _logger;
        private readonly IMediator _mediator;
        private readonly FhirMediator _fhirMediator;
        private readonly IFhirMediatorService _fhirMediatorService;

        public FhirController(ILogger<FhirController> logger, IMediator mediator, IMediator fhirMediator, IFhirMediatorService fhirMediatorService)
        {
            _logger = logger;
            _mediator = mediator;
            _fhirMediator = (FhirMediator) fhirMediator;
            _fhirMediatorService = fhirMediatorService;
        }

        [HttpGet]
        [Route("{resource}")]
        public async Task<ActionResult<Resource>> Resource(string resource)
        {
            string knownResource = resource.GetResourceFromString();
            if (string.IsNullOrWhiteSpace(knownResource))
                return OperationOutcomeResult.CreateOperationOutcomeResult(
                    "Resource not known.",
                    OperationOutcome.IssueSeverity.Error,
                    OperationOutcome.IssueType.NotFound,
                    StatusCodes.Status404NotFound);

            _logger.LogInformation($"Performing Search for {knownResource}...");

            var searchParams = new SearchParams { Count = 50 };
            var request = SearchResourceRequest.CreateInstance(searchParams, knownResource);

            _logger.LogInformation("Searching using mediator with extensions...");
            var searchWithMediatorExtensions = await _mediator.SearchResourceAsync(knownResource, searchParams, CancellationToken.None);

            _logger.LogInformation("Searching using custom mediator...");
            var searchWithCustomMediator = await _fhirMediator.SendSearchResourceRequest(request);

            _logger.LogInformation("Searching using mediator service layer...");
            var searchWithServiceMediator = await _fhirMediatorService.SendSearchResourceRequest(request);

            return FhirResult.CreateInstance(searchWithMediatorExtensions.Resource, StatusCodes.Status200OK);
        }
    }
}