using System.Collections.Concurrent;
using System.Dynamic;
using System.Runtime.CompilerServices;
using codecrafters_redis.Contract;
using codecrafters_redis.Models;

namespace codecrafters_redis.Impl;

public static class RedisCache
{
    private static SortedDictionary<string, RedisCacheValue> map = new();
    private static HashSet<string> idMap = new();
    private static int idKey = 0;
    private static int idValue = 0;
    private static int latestIdSegment = 0;

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
        if (id.Contains("*"))
        {
            id = GetId(id);
        }
        var dict = new Dictionary<string, string>()
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

    private static string GetId(string id)
    {
        var idKey = int.Parse(id.Split("-")[0]);
        if (idKey == 0 && latestIdSegment == 0)
        {
            return "0-1";
        }
        else
        {
            return $"{idKey}-{latestIdSegment++}";
        }

    }

    private static bool IsIdValid(string id, out string message)
    {
        var split = id.Split("-");
        var splitId = new KeyValuePair<int, int>(int.Parse(split[0]), int.Parse(split[1]));
        if (id == "0-0")
        {
            message = "-ERR The ID specified in XADD must be greater than 0-0\r\n";
            return false;
        }
        
        if (idMap.Contains(id) || (idKey != 0 && idValue != 0 && ((splitId.Key == idKey && splitId.Value <= idValue) || (splitId.Key < idKey && splitId.Value > idValue))))
        {
            Console.WriteLine(id);
            Console.WriteLine(idKey);
            Console.WriteLine(idValue);
            message = "-ERR The ID specified in XADD is equal or smaller than the target stream top item\r\n";
            return false;
        }

        idKey = splitId.Key;
        idValue = splitId.Value;
        idMap.Add(id);
        message = string.Empty;
        return true;
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