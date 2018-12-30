namespace TypeFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal static class TsDefinitions
    {
        private static readonly Func<IEnumerable<IEnumerable<Tuple<string, IEnumerable<List<string>>>>>, IEnumerable<string>> TsClassProperties =
            props => props.SelectMany(
                prop => prop.SelectMany(
                    properties => properties.Item2.SelectMany(attr => attr.Select(a => "\t" + $"static readonly {properties.Item1}{a}").ToList())));

        private static readonly Action<StringBuilder, Tuple<string, IEnumerable<IEnumerable<Tuple<string, IEnumerable<List<string>>>>>>> TsClass =
            (sb, type) =>
            {
                sb.AppendLine($"export class {type.Item1} {{");
                TsClassProperties(type.Item2).ToList().ForEach(e => sb.AppendLine(e));
                sb.AppendLine($"}}");
            };

        public static string CreateFor(IEnumerable<Tuple<string, IEnumerable<IEnumerable<Tuple<string, IEnumerable<List<string>>>>>>> types)
        {
            var sb = new StringBuilder();
            sb.AppendLine("/* tslint:disable */");
            sb.AppendLine("/* AUTO GENERATED, DO NOT MODIFY BY HAND */");
            sb.AppendLine(string.Empty);
            types.ToList().ForEach(t => TsClass(sb, t));

            return sb.ToString();
        }
    }
}