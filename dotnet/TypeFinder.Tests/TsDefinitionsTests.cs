namespace TypeFinder.Tests
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TsDefinitionsTests
    {
        [TestMethod]
        public void Create()
        {
            var expectedProperties = new List<string>
                                         {
                                             "PutAnotherPropertyStringLength = 123",
                                             "PutPropertyRangeMin = 33",
                                             "PutPropertyRangeMax = 66",
                                             "PutPropertyIsRequired = true",
                                             "PutPropertyIsCreditCard = true",
                                             "GetSomePropertyIsRequired = true",
                                             "GetSomePropertyMinLength = 55",
                                             "PostCollectionOfNestedTypesIsRequired = true",
                                             "PostSomePropertyIsRequired = true",
                                             "TestNestedPropertyStringLength = 123",
                                             "TestNestedTypeInCollectionPropertyIsRequired = true",
                                             "PostSomePropertyRegularExpression = `^[a-z''-'\\s]{1,3}$`",
                                             "TestNestedTypeInCollectionPropertyRegularExpression = ``",
                                             "TestAnotherNestedTypeInCollectionPropertyMaxLength = 22",
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

            var ts = TsDefinitions.CreateFor(AttributesTranform.For(FindTypes.Find("TypeFinder.Tests.dll"), TsFromAttribute.For));

            expectedProperties.ForEach(c => Assert.IsTrue(ts.Contains(c)));
            expectedClasses.ForEach(c => Assert.IsTrue(ts.Contains($"export class {c}")));
        }
    }
}