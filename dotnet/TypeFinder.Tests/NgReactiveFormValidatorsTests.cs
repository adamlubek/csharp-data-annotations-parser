namespace TypeFinder.Tests
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NgReactiveFormValidatorsTests
    {
        [TestMethod]
        public void Create()
        {
            var expectedProperties = new List<string>
                                         {
                                             "static readonly PutAnotherPropertyValidators = [Validators.minLength(123), Validators.maxLength(123)];",
                                             "static readonly PutPropertyValidators = [Validators.min(33), Validators.max(66), Validators.required];",
                                             "static readonly GetSomePropertyValidators = [Validators.required, Validators.minLength(55)];",
                                             "static readonly PostCollectionOfNestedTypesValidators = [Validators.required];",
                                             "static readonly PostSomePropertyValidators = [Validators.required, Validators.pattern(`^[a-z''-'\\s]{1,3}$`)];",
                                             "static readonly TestNestedPropertyValidators = [Validators.minLength(123), Validators.maxLength(123)];",
                                             "static readonly TestAnotherNestedTypeInCollectionPropertyValidators = [Validators.required, Validators.maxLength(22)];",
                                             "static readonly TestEmailNestedTypeInCollectionPropertyValidators = [Validators.email];",
                                             "static readonly TestNestedTypeInCollectionPropertyValidators = [Validators.required, Validators.pattern(``)];"
                                         };
            var expectedClasses = new List<string>
                                      {
                                          "TypeFinderTestsTestApiTestGetRequest",
                                          "TypeFinderTestsTestApiTestPostRequest",
                                          "TypeFinderTestsTestApiTestPutRequest",
                                          "TypeFinderTestsTestApiTestNestedType",
                                          "TypeFinderTestsTestApiTestNestedTypeInCollection",
                                          "TypeFinderTestsTestApiNamespaceWithDuplicatedNamesTestNestedType"
                                      };

            var ts = NgReactiveFormValidators.CreateFor(
                AttributesTranform.For(FindTypes.Find("TypeFinder.Tests.dll"), NgValidatorsFromAttribute.For));

            expectedProperties.ForEach(c => Assert.IsTrue(ts.Contains(c)));
            expectedClasses.ForEach(c => Assert.IsTrue(ts.Contains($"export class {c}Validators")));
        }
    }
}