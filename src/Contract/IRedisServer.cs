namespace codecrafters_redis.Contract;

public interface IRedisServer
{
    void Start();
    void Process(IAsyncResult asyncResult);
    void Stop();
}