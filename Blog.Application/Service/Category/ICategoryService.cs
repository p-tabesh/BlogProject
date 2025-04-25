using Blog.Application.Model.Category;

namespace Blog.Application.Service.Category;

public interface ICategoryService
{
    IEnumerable<CategoryViewModel> GetAll();
    CategoryViewModel GetById(int id);
}
