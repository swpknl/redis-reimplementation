using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using codecrafters_redis.Contract;
using codecrafters_redis.Models;

namespace codecrafters_redis.Impl;

public static class RedisCache
{
    private static ConcurrentDictionary<string, RedisCacheValue> map = new(); 
    public static IResponse Get(string request)
    {
        var array = request.Split("\r\n");
        var key = array[array.Length - 2];
        var isPresent = map.TryGetValue(key, out RedisCacheValue value);
        if (isPresent)
        {
            if (value.ExpiryDateTime >= DateTime.Now)
            {
                var keyValue = (value.Value as StringValue).Value;
                return new KeyResponse(keyValue);    
            }
            else
            {
                return new NullResponse();
            }    
        }
        else
        {
            return new NullResponse();
        }
    }
    
    public static IResponse XAdd(string request)
    {
        var array = request.Split("\r\n");
        foreach (var element in array) 
        {
            Console.WriteLine(element);
        }
        
        return new NullResponse();
    }

    public static IResponse Type(string request)
    {
        var array = request.Split("\r\n");
        var key = array[array.Length - 2];
        var isPresent = map.TryGetValue(key, out RedisCacheValue value);
        if (isPresent)
        {
            return new TypeResponse(value.Value.ValueType);
        }
        else
        {
            return new TypeResponse("none");
        }
        
    }
    
    public static IResponse Set(string request)
    {
        var array = request.Split("\r\n");
        if (request.ToLower().Contains("px"))
        {
            var key = array[array.Length - 8];
            var value = array[array.Length - 6];
            var timeout = double.Parse(array[array.Length - 2]);
            var datetime = DateTime.Now.AddMilliseconds(timeout);
            map.TryAdd(key, new RedisCacheValue(new StringValue(value), datetime));
        }
        else
        {
            var key = array[array.Length - 4];
            var value = array[array.Length - 2];
            map.TryAdd(key, new RedisCacheValue(new StringValue(value), DateTime.Now.AddYears(1)));    
        }
        
        return new OkResponse();
    }
}