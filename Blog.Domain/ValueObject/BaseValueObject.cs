namespace Blog.Domain.ValueObject;

public class BaseValueObject
{
    public string Value { get; protected set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != this.GetType())
            return false;

        var other = (BaseValueObject)obj;
        return this.Value == other.Value;
    }

    public static bool operator ==(BaseValueObject left, BaseValueObject right)
    {
        return left.Value == right.Value;
    }

    public static bool operator !=(BaseValueObject left, BaseValueObject right)
    {
        return left.Value != right.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public override string ToString()
    {
        return Value;
    }
}
