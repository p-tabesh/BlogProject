using AutoMapper;
using Blog.Domain.IUnitOfWork;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service;

public class BaseService<T> where T : class
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly ILogger<T> Logger;
    protected readonly IMapper Mapper;

    public BaseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<T> logger)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
        Logger = logger;
    }
}
