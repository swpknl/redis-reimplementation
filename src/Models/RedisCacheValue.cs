namespace codecrafters_redis.Models;

public class RedisCacheValue
{
    public string Key { get; set; }
    public DateTime ExpiryDateTime { get; set; }

    public RedisCacheValue(string key, DateTime expiryDateTime)
    {
        this.Key = key;
        this.ExpiryDateTime = expiryDateTime;
    }
}