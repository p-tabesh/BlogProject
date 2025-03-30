using Blog.Application.Category.Service;
using Blog.Application.Service.Category;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Blog.Infrastructure.Repository;
using Blog.Infrastructure.UnitOfWork;
using Microsoft.Identity.Client;

namespace Blog.Web.Extention;

public static class ConfigurationExtention
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
    }
}
