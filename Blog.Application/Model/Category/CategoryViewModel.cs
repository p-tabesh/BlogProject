namespace Blog.Application.Model.Category;

public record CategoryViewModel(int Id, string Title, string Description, int? ParentCategoryId,bool IsActive);

