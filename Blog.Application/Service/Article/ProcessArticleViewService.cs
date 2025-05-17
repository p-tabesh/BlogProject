using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace Blog.Application.Service.Article;

public class ProcessArticleViewService : BackgroundService
{
    private readonly StackExchange.Redis.IDatabase _redis;
    private readonly StackExchange.Redis.IServer _server;
    private readonly IServiceScopeFactory _scopeFactory;

    public ProcessArticleViewService(ConnectionMultiplexer connectionMultiplexer, IServiceScopeFactory factory)
    {
        _redis = connectionMultiplexer.GetDatabase();
        _server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints().First());
        _scopeFactory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var articleList = new List<int>();

            using (var scope = _scopeFactory.CreateScope())
            {
                await foreach (var key in _server.KeysAsync(pattern: "view:*"))
                {
                    var value = await _redis.StringGetAsync(key);
                    if (int.TryParse(value, out int id))
                        articleList.Add(id);
                }

                if (articleList.Count == 0)
                    continue;

                var groupedArticle = articleList.GroupBy(x => x).Select(g => new { Id = g.Key, Count = g.Count() });
                var repo = scope.ServiceProvider.GetRequiredService<IArticleRepository>();
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                foreach (var article in groupedArticle)
                    repo.GetById(article.Id).AddView(article.Count);

                uow.Commit();
            }

            var keys = _server.Keys(pattern: "view:*");

            foreach (var key in keys)
                await _redis.KeyDeleteAsync(key);

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
