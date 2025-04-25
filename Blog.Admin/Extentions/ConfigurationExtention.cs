using Blog.Application.Service.Account;
using Blog.Application.Service.Article;
using Blog.Application.Service.Category;
using Blog.Application.Service.Comment;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Blog.Infrastructure.Repository;
using Blog.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.OpenApi.Models;

namespace Blog.Admin.Extention;

public static partial class ConfigurationExtention
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAdminCategoryService, AdminCategoryService>();
        services.AddScoped<IAdminArticleService, AdminArticleService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAdminCommentService,AdminCommentService>();
    }

    public static void AddBlogSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "Jwt Bearer Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void AddBlogRedis(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString")));

        services.AddStackExchangeRedisCache(
                option =>
                {
                    option.Configuration = configuration.GetConnectionString("RedisConnectionString");
                    option.InstanceName = "BlogAppCache";
                });

        services.AddSingleton<IDistributedCache, RedisCache>();
    }
}


public static partial class ConfigurationExtention
{
    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}