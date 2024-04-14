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
            return new KeyResponse(value.Key);    
        }
        else
        {
            return new NullResponse();
        }
    }

    public static IResponse Set(string request)
    {
        Console.WriteLine(request);
        var array = request.Split("\r\n");
        var key = array[array.Length - 4];
        var value = array[array.Length - 2];
        map.TryAdd(key, new RedisCacheValue(value, DateTime.Now));
        return new OkResponse();
    }
}