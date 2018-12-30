namespace TypeFinder.Tests.TestApi
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TestGetRequest
    {
        public TestNestedType GetNestedType { get; set; }

        [Required]
        [MinLength(55)]
        public string GetSomeProperty { get; set; }
    }

    public class TestPostRequest
    {
        [Required]
        public List<TestNestedTypeInCollection> PostCollectionOfNestedTypes { get; set; }

        [Required]
        [RegularExpression(@"^[a-z''-'\s]{1,3}$")]
        public string PostSomeProperty { get; set; }
    }

    public class TestPutRequest
    {
        [StringLength(123)]
        public string PutAnotherProperty { get; set; }

        [Range(33, 66)]
        [Required]
        [CreditCard]
        public string PutProperty { get; set; }
    }

    public class TestNestedType
    {
        [StringLength(123)]
        public int TestNestedProperty { get; set; }
    }

    public class TestNestedTypeInCollection
    {
        [Required]
        [MaxLength(22)]
        public string TestAnotherNestedTypeInCollectionProperty { get; set; }

        [EmailAddress]
        public string TestEmailNestedTypeInCollectionProperty { get; set; }

        [Required]
        [RegularExpression("")]
        public int TestNestedTypeInCollectionProperty { get; set; }
    }
}

namespace TypeFinder.Tests.TestApi.NamespaceWithDuplicatedNames
{
    using System.ComponentModel.DataAnnotations;

    public class TestNestedType
    {
        [StringLength(123)]
        public int TestNestedProperty { get; set; }
    }

}