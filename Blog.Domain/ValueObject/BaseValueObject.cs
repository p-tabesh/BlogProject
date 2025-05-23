namespace Blog.Domain.ValueObject;

public class BaseValueObject<T>
{
    public string Value { get; protected set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != this.GetType())
            return false;

        var other = (BaseValueObject<T>)obj;
        return this.Value == other.Value;
    }

    public static bool operator ==(BaseValueObject<T> left, BaseValueObject<T> right)
    {
        return left.Value == right.Value;
    }

    public static bool operator !=(BaseValueObject<T> left, BaseValueObject<T> right)
    {
        return left.Value != right.Value;
    }
}
