namespace codecrafters_redis.Contract;

public interface IResponseFactory
{
    IResponse GetResponse(string request);
}