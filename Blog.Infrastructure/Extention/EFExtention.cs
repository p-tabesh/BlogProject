using Blog.Infrastructure.Context;
using Core.Repository.Model.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Blog.Infrastructure.Extention;

public static class EFExtention
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

    public static IQueryable<T> ApplySkipTake<T>(this IQueryable<T> query, int skip, int take)
    {
        if (skip != 0)
        {
            query = query.Skip(skip);
        }

        if (take != 0)
        {
            query = query.Take(take);
        }

        return query;
    }
}
