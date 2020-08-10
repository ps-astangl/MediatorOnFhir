﻿﻿using Hl7.Fhir.Rest;
using MediatR;

namespace MediatorOnFhir.Messages
{
    public class SearchResourceRequest : IRequest<SearchResourceResponse>
    {
        public static SearchResourceRequest CreateInstance(SearchParams searchParams, string resourceType)
        {
            return new SearchResourceRequest(searchParams, resourceType);
        }

        private SearchResourceRequest(SearchParams searchParams, string resourceType)
        {
            SearchParams = searchParams;
            ResourceType = resourceType;
        }
        public SearchParams SearchParams { get; set; }
        public string ResourceType { get; set; }
    }
}