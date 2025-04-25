using AutoMapper;
using Blog.Application.Model.Category;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Service.Category;

public class AdminCategoryService : BaseService<AdminCategoryService>, IAdminCategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public AdminCategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<AdminCategoryService> logger)
        : base(unitOfWork, mapper, logger)
    {
        _categoryRepository = categoryRepository;
    }
    public int CreateCategory(CreateCategory request)
    {
        var category = Mapper.Map<Domain.Entity.Category>(request);
        _categoryRepository.Add(category);
        UnitOfWork.Commit();
        return category.Id;
    }

    public void DeActiveCategory(int categoryId)
    {
        var category = _categoryRepository.GetById(categoryId);
        category.DeActive();
        _categoryRepository.Update(category);
        UnitOfWork.Commit();
    }

    public void EditCategory(EditCategoryRequest request)
    {
        var category = _categoryRepository.GetById(request.CategoryId);
        category.Edit(request.Title, request.Description);
        _categoryRepository.Update(category);
        UnitOfWork.Commit();
    }

    public IEnumerable<CategoryViewModel> GetAllCategories()
    {
        var models = Mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll());
        return models;
    }
}
