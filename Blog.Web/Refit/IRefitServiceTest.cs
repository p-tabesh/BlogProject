using Refit;


namespace Blog.Web.Refit;
public interface IRefitServiceTest
{
    [Get("/api/articles/2")]
    Task<string> GetTest();

}
