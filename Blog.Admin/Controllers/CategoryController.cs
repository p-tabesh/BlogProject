using Blog.Application.Service.Category;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Admin.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController : Controller
{
    IAdminCategoryService _categoryService;

    public CategoryController(IAdminCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _categoryService.GetAllCategories();
        return Ok(categories);
    }
}
