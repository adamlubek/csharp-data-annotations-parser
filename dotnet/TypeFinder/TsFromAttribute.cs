namespace TypeFinder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    internal class TsFromAttribute
    {
        private static readonly Func<CustomAttributeData, object, string> FormatWithName = (a, v) =>
            a.AttributeType.Name.Replace("Attribute", string.Empty) + v + ";";

        private static readonly Func<CustomAttributeData, List<string>> RangeAttrHandler = a =>
            new List<string>
                {
                    FormatWithName(a, $"Min = {a.ConstructorArguments[0].Value}"),
                    FormatWithName(a, $"Max = {a.ConstructorArguments[1].Value}")
                };

        private static readonly Func<CustomAttributeData, List<string>> RegExprAttrHandler = a =>
            new List<string> { FormatWithName(a, " = `" + a.ConstructorArguments[0].Value + "`") };

        private static readonly Func<CustomAttributeData, List<string>> RequiredAttrHandler =
            a => new List<string> { "Is" + FormatWithName(a, " = true") };

        private static readonly Func<CustomAttributeData, List<string>> StringLengthAttrHandler =
            a => new List<string> { FormatWithName(a, " = " + a.ConstructorArguments[0].Value) };

        public static List<string> For(CustomAttributeData a)
        {
            var handlers = new Dictionary<Type, Func<CustomAttributeData, List<string>>>
                               {
                                   { typeof(RangeAttribute), RangeAttrHandler },
                                   {
                                       typeof(RegularExpressionAttribute),
                                       RegExprAttrHandler
                                   }
                               };

            new List<Type>
                {
                    typeof(RequiredAttribute),
                    typeof(CreditCardAttribute),
                    typeof(EmailAddressAttribute),
                    typeof(PhoneAttribute),
                    typeof(UrlAttribute)
                }.ForEach(attribute => handlers.Add(attribute, RequiredAttrHandler));

            new List<Type> { typeof(StringLengthAttribute), typeof(MinLengthAttribute), typeof(MaxLengthAttribute) }.ForEach(
                attribute => handlers.Add(attribute, StringLengthAttrHandler));

            return handlers.ContainsKey(a.AttributeType) ? handlers[a.AttributeType](a) : new List<string>();
        }
    }
}