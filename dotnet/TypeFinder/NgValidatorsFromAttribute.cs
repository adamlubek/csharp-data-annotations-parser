namespace TypeFinder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    internal class NgValidatorsFromAttribute
    {
        private static readonly Func<CustomAttributeData, List<string>> EmailAttrHandler = a => new List<string> { "Validators.email" };

        private static readonly Func<CustomAttributeData, List<string>> RangeAttrHandler = a =>
            new List<string> { $"Validators.min({a.ConstructorArguments[0].Value})", $"Validators.max({a.ConstructorArguments[1].Value})" };

        private static readonly Func<CustomAttributeData, List<string>> RegExprAttrHandler = a =>
            new List<string> { "Validators.pattern(`" + a.ConstructorArguments[0].Value + "`)" };

        private static readonly Func<CustomAttributeData, List<string>> RequiredAttrHandler = a => new List<string> { "Validators.required" };

        private static readonly Func<CustomAttributeData, List<string>> StringLengthAttrHandler = a =>
            new List<string>
                {
                    $"Validators.minLength({a.ConstructorArguments[0].Value})",
                    $"Validators.maxLength({a.ConstructorArguments[0].Value})"
                };

        private static readonly Func<CustomAttributeData, List<string>> StringMaxLengthAttrHandler =
            a => new List<string> { $"Validators.maxLength({a.ConstructorArguments[0].Value})" };

        private static readonly Func<CustomAttributeData, List<string>> StringMinLengthAttrHandler =
            a => new List<string> { $"Validators.minLength({a.ConstructorArguments[0].Value})" };

        public static List<string> For(CustomAttributeData a)
        {
            var handlers = new Dictionary<Type, Func<CustomAttributeData, List<string>>>
                               {
                                   { typeof(EmailAddressAttribute), EmailAttrHandler },
                                   { typeof(RangeAttribute), RangeAttrHandler },
                                   {
                                       typeof(RegularExpressionAttribute),
                                       RegExprAttrHandler
                                   },
                                   {
                                       typeof(StringLengthAttribute),
                                       StringLengthAttrHandler
                                   },
                                   {
                                       typeof(MinLengthAttribute),
                                       StringMinLengthAttrHandler
                                   },
                                   {
                                       typeof(MaxLengthAttribute),
                                       StringMaxLengthAttrHandler
                                   }
                               };

            new List<Type> { typeof(RequiredAttribute), typeof(CreditCardAttribute), typeof(PhoneAttribute), typeof(UrlAttribute) }.ForEach(
                attribute => handlers.Add(attribute, RequiredAttrHandler));

            return handlers.ContainsKey(a.AttributeType) ? handlers[a.AttributeType](a) : new List<string>();
        }
    }
}