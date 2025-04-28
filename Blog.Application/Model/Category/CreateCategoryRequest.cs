namespace Blog.Application.Model.Category;

public record CreateCategoryRequest(string Name, string Description, int? ParentCategoryId);