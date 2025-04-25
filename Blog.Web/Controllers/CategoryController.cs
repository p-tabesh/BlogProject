using Blog.Application.Model.Category;
using Blog.Application.Service.Category;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : Controller
{
    private ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _categoryService.GetAll();
        return Ok(categories);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetCategory(int id)
    {
        var category = _categoryService.GetById(id);
        return Ok(category);
    }
}
