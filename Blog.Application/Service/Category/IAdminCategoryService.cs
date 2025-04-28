using Blog.Application.Model.Category;

namespace Blog.Application.Service.Category;

public interface IAdminCategoryService
{
    IEnumerable<CategoryViewModel> GetAllCategories();
    int CreateCategory(CreateCategoryRequest request);
    void EditCategory(EditCategoryRequest request);
    void DeActiveCategory(int categoryId);
}
