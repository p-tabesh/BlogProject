using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Test;

public static class TestExtention
{
    public static IServiceCollection AddInMemoryBlogDbContext(this IServiceCollection services)
    {
        var descriptors = services.Where(d => d.ServiceType == typeof(DbContextOptions<BlogDbContext>)).ToList();

        foreach (var descriptor in descriptors)
        {
            if (descriptor != null)
                services.Remove(descriptor);
        }

        services.AddDbContext<BlogDbContext>(option =>
        {
            option.UseInMemoryDatabase("InMemoryDbBlogTest");
        });

        return services;
    }
}
