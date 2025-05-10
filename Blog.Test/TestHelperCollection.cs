using Microsoft.AspNetCore.Mvc.Testing;

namespace Blog.Test;

[CollectionDefinition("TestCollection")]
public class TestHelperCollection : ICollectionFixture<TestHelper>
{

}
