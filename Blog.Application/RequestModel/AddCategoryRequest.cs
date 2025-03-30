namespace Blog.Application.RequestModel;

public record AddCategoryRequest(string Name, string Description, int? ParentCategoryId);