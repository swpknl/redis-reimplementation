namespace codecrafters_redis.Models;

public class RedisCacheValue
{
    public BaseValue Value { get; set; }
    public DateTime ExpiryDateTime { get; set; }

    public RedisCacheValue(BaseValue value, DateTime expiryDateTime)
    {
        this.Value = value;
        this.ExpiryDateTime = expiryDateTime;
    }
}