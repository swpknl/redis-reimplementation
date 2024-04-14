namespace codecrafters_redis.Models;

public enum RequestType
{
    PING,
    ECHO,
    GET,
    SET,
    NULL,
    TYPE,
    XADD
}