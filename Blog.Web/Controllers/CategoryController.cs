using Blog.Application.RequestModel;
using Blog.Application.Service.Category;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    private ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddCategory(AddCategoryRequest request)
    {
        _categoryService.Add(request);
        return Ok();
    }
}
