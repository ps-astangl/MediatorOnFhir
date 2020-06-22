#nullable enable
using System;
using System.Linq;
using Hl7.Fhir.Model;

namespace MediatorOnFhir.Extensions
{
    public static class ResourceExtensions
    {
        private static Array KnownResources => Enum.GetValues(typeof(ResourceType));

        public static string? GetResourceFromString(this string resource)
        {
            return KnownResources.Cast<object?>()
                .Where(knownResource =>
                    knownResource?.ToString()?.Equals(resource, StringComparison.InvariantCultureIgnoreCase) ?? false)
                .Select(knownResource => knownResource?.ToString())
                .FirstOrDefault();
        }
    }
}