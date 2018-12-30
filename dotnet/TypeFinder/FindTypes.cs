namespace TypeFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class FindTypes
    {
        public static List<IGrouping<string, IEnumerable<Tuple<string, IEnumerable<CustomAttributeData>>>>> Find(string assemblyFilePath)
        {
            var typesWithAttributes = RequestsTypesWithPropertiesAndAttributes.Find(assemblyFilePath).ToList();

            var typesWithValidationAttributes = typesWithAttributes.Where(types => types.Item2.Any(property => property.Item2.Any()));

            return typesWithValidationAttributes.GroupBy(
                r => r.Item1.FullName.Replace("+", string.Empty).Replace(".", string.Empty), // .Split('.').Last().Replace("+", string.Empty),
                request => request.Item2.Where(properties => properties.Item2.Any())
                    .Select(properties => Tuple.Create(properties.Item1.Name, properties.Item2))).ToList();
        }
    }
}