using Microsoft.AspNetCore.Mvc.Testing;

namespace Blog.Test.Helper;

[CollectionDefinition("TestCollection")]
public class TestHelperCollection : ICollectionFixture<TestHelper>
{

}
