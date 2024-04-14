using System.Reflection.PortableExecutable;

namespace codecrafters_redis.Models;

public class BaseValue
{
    public string ValueType { get; set; }

    public BaseValue(string valueType)
    {
        this.ValueType = valueType;
    }
}