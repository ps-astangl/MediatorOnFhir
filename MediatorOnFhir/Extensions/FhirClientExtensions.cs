using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Task = System.Threading.Tasks.Task;

namespace MediatorOnFhir.Extensions
{
    public static class FhirClientExtensions
    {
        public static async Task<Resource> PerformSearch(this IFhirClient fhirClient, SearchParams searchParams, string resourceType)
        {
            var pagedResult = await Task.Run(() =>
            {
                var pagedResultTask = fhirClient.SearchAsync(searchParams, resourceType);
                return pagedResultTask;
            });

            Bundle bundle = pagedResult.DeepCopy() as Bundle;
            int count = 1;
            do
            {
                pagedResult = await fhirClient.ContinueAsync(pagedResult);
                if (pagedResult?.Entry == null) continue;
                bundle?.Entry?.AddRange(pagedResult.Entry);
                count++;
            } while (pagedResult != null);

            if (bundle != null)
                bundle.Total = bundle.Entry?.Count ?? 0;
            return bundle;
        }
    }
}