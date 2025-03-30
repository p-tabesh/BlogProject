using Blog.Application.RequestModel;
using Blog.Domain.Entity;

namespace Blog.Application.Service.Category;

public interface ICategoryService
{
    void Add(AddCategoryRequest request);
}
