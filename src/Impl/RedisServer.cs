using System.Net;
using System.Net.Sockets;
using System.Text;
using codecrafters_redis.Contract;

namespace codecrafters_redis;

public class RedisServer : IRedisServer
{
    private TcpListener server;

    public void Start()
    {
        this.server = new TcpListener(IPAddress.Any, 6379);
        this.server.Start();
        while (true)
        {
            server.BeginAcceptSocket(asyncResult => Process(asyncResult), null);
        }
    }

    public void Process(IAsyncResult asyncResult)
    {
        var socket = this.server.EndAcceptSocket(asyncResult);
        while (socket.Connected)
        {
            var bytes = new byte[1024];
            socket.Receive(bytes);
            var request = Encoding.UTF8.GetString(bytes);
            var redisRequestParser = new RequestParser();
            var response = redisRequestParser.Parse(request);
            socket.Send(response.GetByteResponse());
        }
    }

    public void Stop()
    {
        this.server.Stop();
    }
}