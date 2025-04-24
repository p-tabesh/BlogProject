using Blog.Infrastructure.Context;
using Core.Repository.Model.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System.Linq.Expressions;

namespace Blog.Infrastructure.Extention;

public static class Extention
{
    public static void AddBlogDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlogDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlConnectionString"));
        });
    }

    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
    {
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return query;
    }

    public static IQueryable<T> Where<T>(this IQueryable<T> query, Specification<T> specification)
    {
        if (specification != null)
        {
            return query.Where(specification.Expression);
        }
        return query;
    }
}
