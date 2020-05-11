using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using MediatorOnFhir.Extensions;
using MediatR;
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
        public async Task<ActionResult<Resource>> Resource()
        {
            _logger.LogInformation("Performing Search");
             var searchResourceAsync = await _mediator.SearchResourceAsync("Flag", new SearchParams(), CancellationToken.None);
             return new OkObjectResult(searchResourceAsync.Resource.ToJson());
        }
    }
}