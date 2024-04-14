using System.Net.Sockets;

namespace codecrafters_redis.Contract;

public interface IRedisServer
{
    Task Start();
    Task Process(Socket socket);
    Task Stop();
}