namespace codecrafters_redis.Models;

public class StringValue : BaseValue
{
    public string Value { get; set; }

    public StringValue(string value) : base("string")
    {
        this.Value = value;
    }
}