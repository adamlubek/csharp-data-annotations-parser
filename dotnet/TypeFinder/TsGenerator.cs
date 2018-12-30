namespace TypeFinder
{
    public static class TsGenerator
    {
        public static void Generate(string assemblyFilePath, string tsConstantsDestination, string ngReactiveFormValidatorsDestination)
        {
            var types = FindTypes.Find(assemblyFilePath);

            if (!string.IsNullOrWhiteSpace(tsConstantsDestination))
                System.IO.File.WriteAllText(tsConstantsDestination, TsDefinitions.CreateFor(AttributesTranform.For(types, TsFromAttribute.For)));
            if (!string.IsNullOrWhiteSpace(ngReactiveFormValidatorsDestination))
                System.IO.File.WriteAllText(
                    ngReactiveFormValidatorsDestination,
                    NgReactiveFormValidators.CreateFor(AttributesTranform.For(types, NgValidatorsFromAttribute.For)));
        }
    }
}
