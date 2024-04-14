namespace codecrafters_redis.Models;

public class RedisCacheValue
{
    public string Value { get; set; }
    public DateTime ExpiryDateTime { get; set; }

    public RedisCacheValue(string key, DateTime expiryDateTime)
    {
        this.Value = key;
        this.ExpiryDateTime = expiryDateTime;
    }
}