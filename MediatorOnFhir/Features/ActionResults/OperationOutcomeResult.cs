using System;
using System.Collections.Generic;
using System.Net;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatorOnFhir.Features.ActionResults
{
    public class OperationOutcomeResult : ObjectResult
    {
        public static OperationOutcomeResult CreateInstanceFromOperationOutcome(OperationOutcome operationOutcome, int statusCodes)
        {
            return new OperationOutcomeResult(operationOutcome, statusCodes);
        }
        public static OperationOutcomeResult CreateOperationOutcomeResult(string message,
            OperationOutcome.IssueSeverity issueSeverity, OperationOutcome.IssueType issueType,
            int statusCode)
        {
            return new OperationOutcomeResult(
                new OperationOutcome
                {
                    Id = Guid.NewGuid().ToString(),
                    Issue = new List<OperationOutcome.IssueComponent>
                    {
                        new OperationOutcome.IssueComponent
                        {
                            Severity = issueSeverity,
                            Code = issueType,
                            Diagnostics = message
                        }
                    }
                }, statusCode);
        }

        public OperationOutcomeResult(OperationOutcome operationOutcome, int statusCode) : base(operationOutcome)
        {
            StatusCode = statusCode;
        }
    }
}