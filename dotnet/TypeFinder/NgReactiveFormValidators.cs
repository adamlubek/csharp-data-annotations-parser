namespace TypeFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal static class NgReactiveFormValidators
    {
        private static readonly Action<StringBuilder, Tuple<string, IEnumerable<IEnumerable<Tuple<string, IEnumerable<List<string>>>>>>> NgValidators
            = (sb, type) =>
            {
                sb.AppendLine($"export class {type.Item1}Validators {{");
                type.Item2.ToList().ForEach(
                    properties => properties.ToList().ForEach(
                        property =>
                        {
                            var validators = property.Item2.SelectMany(e => e).ToList();
                            if (validators.Any())
                            {
                                sb.AppendLine(
                                        "\t"
                                        + $"static readonly {property.Item1}Validators = [{validators.Distinct().Aggregate((v1, v2) => $"{v1}, {v2}")}];");
                            }
                        }));

                sb.AppendLine($"}}");
            };

        public static string CreateFor(IEnumerable<Tuple<string, IEnumerable<IEnumerable<Tuple<string, IEnumerable<List<string>>>>>>> types)
        {
            var sb = new StringBuilder();
            sb.AppendLine("/* tslint:disable */");
            sb.AppendLine("/* AUTO GENERATED, DO NOT MODIFY BY HAND */");
            sb.AppendLine("import { Validators } from '@angular/forms';");
            sb.AppendLine(string.Empty);

            types.ToList().ForEach(t => NgValidators(sb, t));

            return sb.ToString();
        }
    }
}
