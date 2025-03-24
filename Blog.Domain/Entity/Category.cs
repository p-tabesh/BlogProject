namespace Blog.Domain.Entity;

public class Category : RootEntity<int>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int? ParentCategoryId { get; private set; }
}
