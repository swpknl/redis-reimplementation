namespace codecrafters_redis.Models;

public class StreamValue : BaseValue
{
    public string Id { get; set; }
    public Dictionary<string, string> Map { get; set; }

    public StreamValue(string id, Dictionary<string, string> map) : base("stream")
    {
        this.Id = id;
        this.Map = map;
    }
}