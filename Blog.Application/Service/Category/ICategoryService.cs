using Blog.Application.Model.Category;

namespace Blog.Application.Service.Category;

public interface ICategoryService
{
    int CreateCategory(AddCategoryRequest request);
}
