using System.Text;

namespace codecrafters_redis.Models;

public class RequestResponse
{
    public string Response { get; set; }
    public byte[] ByteResponse { get; set; }
    
    public RequestResponse(string response)
    {
        this.Response = response;
    }

    public byte[] GetByteResponse()
    {
        return Encoding.UTF8.GetBytes(this.Response);
    }
}