using AutoMapper;
using Blog.Application.Model.Category;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service.Category;

public class CategoryService : BaseService<CategoryService>,ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IUnitOfWork unitOfWork,ICategoryRepository categoryRepository, IMapper mapper,ILogger<CategoryService> logger)
        :base(unitOfWork, mapper, logger)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<CategoryViewModel> GetAll()
    {
        var models = Mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll());
        return models;
    }

    public CategoryViewModel GetById(int id)
    {
        var model = Mapper.Map<CategoryViewModel>(_categoryRepository.GetById(id));
        return model;
    }
}
