namespace Blog.Domain.Entity;

public class Category : RootEntity<int>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int? ParentCategoryId { get; private set; }
    public Category? ParentCategory { get; private set; }
    public List<Category> ChildCategories { get; private set; }
}
