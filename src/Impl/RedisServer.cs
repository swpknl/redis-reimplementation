using System.Net;
using System.Net.Sockets;
using System.Text;
using codecrafters_redis.Contract;

namespace codecrafters_redis;

public class RedisServer : IRedisServer
{
    private TcpListener server;

    public async Task Start()
    {
        this.server = new TcpListener(IPAddress.Any, 6379);
        this.server.Start();
        while (true)
        {
            var socket = await server.AcceptSocketAsync().ConfigureAwait(true);
            await Process(socket);
        }
    }

    public async Task Process(Socket socket)
    {
        while (socket.Connected)
        {
            var array = new byte[10000];
            var bytes = new ArraySegment<byte>(array);
            await socket.ReceiveAsync(bytes, SocketFlags.None);
            var request = Encoding.UTF8.GetString(bytes);
            var redisRequestParser = new RequestParser();
            var response = redisRequestParser.Parse(request);
            await socket.SendAsync(response.GetByteResponse(), SocketFlags.None);
        }
    }

    public Task Stop()
    {
        this.server.Stop();
        return Task.CompletedTask;
    }
}