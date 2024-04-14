using System.Text;

namespace codecrafters_redis.Models;

public class RequestResponse
{
    private string response { get; set; }
    
    public RequestResponse(string response)
    {
        this.response = response;
    }

    public ArraySegment<byte> GetByteResponse()
    {
        return new ArraySegment<byte>(Encoding.UTF8.GetBytes(this.response));
    }
}