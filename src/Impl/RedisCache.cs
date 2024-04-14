using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using codecrafters_redis.Contract;

namespace codecrafters_redis.Impl;

public static class RedisCache
{
    private static ConcurrentDictionary<string, string> map = new(); 
    public static IResponse Get(string request)
    {
        Console.WriteLine(request);
        var array = request.Split("\r\n");
        var key = array[array.Length - 2];
        return new KeyResponse(key);
    }

    public static IResponse Set(string request)
    {
        var array = request.Split("\r\n");
        var key = array[array.Length - 4];
        var value = array[array.Length - 2];
        map.TryAdd(key, value);
        return new OkResponse();
    }
}