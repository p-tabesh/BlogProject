namespace Blog.Domain.Entity;

public class Category : RootEntity<int>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int? ParentCategoryId { get; private set; }
    public Category? ParentCategory { get; private set; }
    public List<Category> ChildCategories { get; private set; }

    private Category() { }

    public Category(string name, string description, int? parentCategoryId)
    {
        Name = string.IsNullOrEmpty(name) ? throw new ArgumentException("Invalid input") : name;
        Description = string.IsNullOrEmpty(description) ? throw new ArgumentException("Invalid input") : description;
        ParentCategoryId = parentCategoryId;
    }
}
