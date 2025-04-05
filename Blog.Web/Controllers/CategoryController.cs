using Blog.Application.Model.Category;
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
    public IActionResult AddCategory(AddCategoryRequest request)
    {
        _categoryService.AddCategory(request);
        return Ok();
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        return Ok();
    }
}
