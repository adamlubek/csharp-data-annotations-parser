namespace TypeFinder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public class RequestsTypesWithPropertiesAndAttributes
    {
        public static IEnumerable<Tuple<Type, IEnumerable<Tuple<PropertyInfo, IEnumerable<CustomAttributeData>>>>> Find(string assemblyFilePath) =>
            Assembly.LoadFrom(assemblyFilePath).GetTypes().Select(type => Tuple.Create(type, AttributesOfRequestProperties(type)));

        private static IEnumerable<Tuple<PropertyInfo, IEnumerable<CustomAttributeData>>> AttributesOfRequestProperties(Type type) =>
            type.GetTypeInfo().DeclaredProperties.Select(
                properttyInfo => Tuple.Create(properttyInfo, properttyInfo.CustomAttributes.Where(IsValidationAttribute)));

        private static bool IsValidationAttribute(CustomAttributeData attribute) => attribute.AttributeType.IsSubclassOf(typeof(ValidationAttribute));
    }
}
