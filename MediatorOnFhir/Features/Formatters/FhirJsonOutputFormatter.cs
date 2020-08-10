using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using EnsureThat;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;

namespace MediatorOnFhir.Features.Formatters
{
    public class FhirJsonOutputFormatter : TextOutputFormatter
    {
        public FhirJsonOutputFormatter()
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            foreach (var mediaHeader in ContentType.JSON_CONTENT_HEADERS)
            {
                SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(mediaHeader)?.MediaType);
            }
        }
        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return context.Object is Resource && base.CanWriteResult(context);
        }


        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var resource = context.Object as Resource;
            context.HttpContext.Response.ContentType = ContentType.JSON_CONTENT_HEADER;
            await context.HttpContext.Response.WriteAsync(resource.ToJson());
        }
    }
}