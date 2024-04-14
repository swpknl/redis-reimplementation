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
        string key = array[4];
        string id = array[6];
        var dict= new Dictionary<string, string>()
        {
            { array[8], array[10] }
        };
        if (IsIdValid(id, out string errorMessage))
        {
            map.TryAdd(key, new RedisCacheValue(new StreamValue(id, dict), DateTime.Now.AddYears(1)));
            return new KeyResponse(id);    
        }
        else
        {
            return new ErrorResponse(errorMessage);
        }
    }

    private static bool IsIdValid(string id, out string message)
    {
        var split = id.Split("-");
        var splitId = new KeyValuePair<int, int>(int.Parse(split[0]), int.Parse(split[1]));
        var ids = map.Values.OrderByDescending(x => x.ExpiryDateTime).Where(x => x.Value.ValueType == "stream").Select(
            x =>
            {
                var arr = (x.Value as StreamValue).Id.Split("-");
                return new KeyValuePair<int, int>(int.Parse(arr[0]), int.Parse(arr[1]));
            });
        var dict = new Dictionary<int, int>(ids);
        if (splitId.Key < 1 && splitId.Value < 1)
        {
            message = "-ERR The ID specified in XADD must be greater than 0-0\r\n";
            return false;
        }
        else if (CheckTopItem(splitId, dict))
        {
            message = "-ERR The ID specified in XADD is equal or smaller than the target stream top item\r\n";
            return false;
        }

        message = string.Empty;
        return true;
    }

    private static bool CheckTopItem(KeyValuePair<int, int> splitId, Dictionary<int, int> dict)
    {
        var top = dict.First();
        return (splitId.Key <= top.Key && splitId.Value == top.Value); 
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