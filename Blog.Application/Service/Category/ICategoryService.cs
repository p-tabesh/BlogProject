using Blog.Application.Model.Category;

namespace Blog.Application.Service.Category;

public interface ICategoryService
{
    IEnumerable<CategoryViewModel> GetAll();
    int CreateCategory(AddCategoryRequest request);
    void EditCategory(EditCategoryRequest request);
    void DeActiveCategory(int categoryId);
}
