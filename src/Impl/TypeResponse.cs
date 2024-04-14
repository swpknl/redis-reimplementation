using codecrafters_redis.Contract;

namespace codecrafters_redis.Impl;

public class TypeResponse : IResponse
{
    private string valueType;
    public TypeResponse(string type)
    {
        this.valueType = type;
    }
    
    public string GetResponse()
    {
        return $"+{this.valueType}\r\n";
    }
}