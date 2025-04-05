using Blog.Application.Model.Category;
using Blog.Domain.IRepository;
using Blog.Domain.IUnitOfWork;

namespace Blog.Application.Service.Category;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public void AddCategory(AddCategoryRequest request)
    {
        var category = new Domain.Entity.Category(request.Name, request.Description,request.ParentCategoryId);
        _categoryRepository.Add(category);
        _unitOfWork.Commit();
    }
}
