using System.Net;
using System.Net.Sockets;
using System.Text;
using codecrafters_redis;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Starting server");
var server = new RedisServer();
await server.Start();