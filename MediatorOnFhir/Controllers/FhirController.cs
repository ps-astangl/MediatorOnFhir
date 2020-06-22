using System;
using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using MediatorOnFhir.Extensions;
using MediatorOnFhir.Features.ActionResults;
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

        public FhirController(ILogger<FhirController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{resource}")]
        public async Task<ActionResult<Resource>> Resource(string resource)
        {
            // TODO: Can be made into a decorator for the controller
            string knownResource = resource.GetResourceFromString();
            if (string.IsNullOrWhiteSpace(knownResource))
                return OperationOutcomeResult.CreateOperationOutcomeResult(
                    "Resource not known.",
                    OperationOutcome.IssueSeverity.Error,
                    OperationOutcome.IssueType.NotFound,
                    StatusCodes.Status404NotFound);

            _logger.LogInformation($"Performing Search for {knownResource}...");

            // TODO: Can be parsed from query
            var searchParams = new SearchParams();

            var searchResourceAsync = await _mediator.SearchResourceAsync(knownResource, searchParams, CancellationToken.None);
            return FhirResult.CreateInstance(searchResourceAsync.Resource, StatusCodes.Status200OK);
        }
    }
}