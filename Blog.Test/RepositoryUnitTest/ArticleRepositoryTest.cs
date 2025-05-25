using Blog.Domain.Entity;
using Blog.Domain.IRepository;
using Blog.Domain.Specifications;
using Core.Repository.Model.Specifications;
using System.Linq;

namespace Blog.Test.RepositoryUnitTest;


public class MockFactory
{
    public static Mock<IArticleRepository> GetArticleRepositoryMock()
    {
        var articles = new List<Article>()
        {
            new Article("header 1","title 1","text 1",new List<string> { ""},"link 1",DateTime.Now,5,1),
            new Article("header 2","title 2","text 2",new List<string> { ""},"link 2",DateTime.Now,4,2),
            new Article("header 3","title 3","text 3",new List<string> { ""},"link 3",DateTime.Now,3,3),
            new Article("header 4","title 4","text 4",new List<string> { ""},"link 4",DateTime.Now,2,4),
            new Article("header 5","title 5","text 5",new List<string> { ""},"link 5",DateTime.Now,1,5),
        };

        var mock = new Mock<IArticleRepository>();

        for (int i = 0; i < 3; i++)
            articles[i].Accept();


        mock.Setup(m => m.GetAll())
            .Returns(() => articles);

        mock.Setup(m => m.GetWithSpecification(It.IsAny<Specification<Article>>()))
            .Returns((Specification<Article> specification) => articles.Where(specification.Expression.Compile()));

        return mock;
    }
}
public class ArticleRepositoryTest
{
    [Fact]
    public void GetWithSpecification_ShouldNotEmpty()
    {
        // Arrange
        var repository = MockFactory.GetArticleRepositoryMock().Object;

        // Act
        var result = repository.GetWithSpecification(new PublishedArticleSpecification());

        // Assert
        Assert.NotEmpty(result);
    }
}
