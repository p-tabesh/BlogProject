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

    public int CreateCategory(AddCategoryRequest request)
    {
        var category = new Domain.Entity.Category(request.Name, request.Description,request.ParentCategoryId);
        _categoryRepository.Add(category);
        _unitOfWork.Commit();

        return category.Id;
    }

    public void DeActiveCategory(int categoryId)
    {
        var category = _categoryRepository.GetById(categoryId);
        category.DeActiveCategory();
        _categoryRepository.Update(category);
        _unitOfWork.Commit();
    }

    public void EditCategory(EditCategoryRequest request)
    {
        var category = _categoryRepository.GetById(request.CategoryId);
        category.EditCategory(request.Title, request.Description);
        _categoryRepository.Update(category);
        _unitOfWork.Commit();
    }

    public IEnumerable<CategoryViewModel> GetAll()
    {
        throw new NotImplementedException();
    }
}
