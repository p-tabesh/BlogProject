namespace Blog.Application.Model.Category;

public record AddCategoryRequest(string Name, string Description, int? ParentCategoryId);