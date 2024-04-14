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
        if (value.ExpiryDateTime >= DateTime.Now)
        {
            return new KeyResponse(value.Value);    
        }
        else
        {
            return new NullResponse();
        }
    }

    public static IResponse Set(string request)
    {
        var array = request.Split("\r\n");
        Console.WriteLine(array);
        if (request.ToLower().Contains("px"))
        {
            var key = array[array.Length - 6];
            Console.WriteLine(key);
            var value = array[array.Length - 4];
            Console.WriteLine(value);
            var timeout = double.Parse(array[array.Length - 2]);
            Console.WriteLine(timeout);
            var datetime = DateTime.Now.AddMilliseconds(timeout);
            map.TryAdd(key, new RedisCacheValue(value, datetime));
        }
        else
        {
            var key = array[array.Length - 4];
            var value = array[array.Length - 2];
            map.TryAdd(key, new RedisCacheValue(value, DateTime.Now.AddYears(1)));    
        }
        
        return new OkResponse();
    }
}