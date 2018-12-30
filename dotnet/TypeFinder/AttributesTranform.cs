namespace TypeFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class AttributesTranform
    {
        public static IEnumerable<Tuple<string, IEnumerable<IEnumerable<Tuple<string, IEnumerable<List<string>>>>>>> For(
            List<IGrouping<string, IEnumerable<Tuple<string, IEnumerable<CustomAttributeData>>>>> ts,
            Func<CustomAttributeData, List<string>> transformer) =>
            ts.Select(
                t => Tuple.Create(
                    t.Key,
                    t.ToList().Select(p => p.ToList().Select(properties => Tuple.Create(properties.Item1, properties.Item2.Select(transformer))))));
    }
}