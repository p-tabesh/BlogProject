using Blog.Application.Model.Category;
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

    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryRequest request)
    {
        var id = _categoryService.CreateCategory(request);
        return CreatedAtAction(nameof(CreateCategory), new { Id = id });
    }

    [HttpPut]
    [Route("{id}/deActive")]
    public IActionResult DeActiveCategory(int id)
    {
        _categoryService.DeActiveCategory(id);
        return Ok();    
    }    
}
