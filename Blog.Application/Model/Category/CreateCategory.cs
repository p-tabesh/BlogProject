namespace Blog.Application.Model.Category;

public record CreateCategory(string Name, string Description, int? ParentCategoryId);