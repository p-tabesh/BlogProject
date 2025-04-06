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

    [HttpPost]
    public IActionResult CreateCategory(AddCategoryRequest request)
    {
        var id = _categoryService.CreateCategory(request);
        return CreatedAtAction(nameof(CreateCategory), new { Id = id });
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetCategory(int id)
    {

        return Ok();
    }
}
