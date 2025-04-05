using Blog.Application.Model.Category;

namespace Blog.Application.Service.Category;

public interface ICategoryService
{
    void AddCategory(AddCategoryRequest request);
}
