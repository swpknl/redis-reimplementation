using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new TcpListener(IPAddress.Any, 6379);
server.Start();
server.BeginAcceptSocket(ac => SendReply(ac), server); // wait for client

void SendReply(IAsyncResult ar)
{
    var server = (TcpListener)ar.AsyncState;
    var socket = server.EndAcceptSocket(ar);
    var bytes = new byte[1024];
    while (socket.Connected)
    {
        var requestData = new byte[1024];
        socket.Receive(bytes);
        socket.Send(Encoding.UTF8.GetBytes("+PONG\r\n"));
    }
}