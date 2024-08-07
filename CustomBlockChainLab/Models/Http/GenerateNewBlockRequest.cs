namespace CustomBlockChainLab.Models.Http;

public class GenerateNewBlockRequest
{
    public string Data { get; set; }

    public GenerateNewBlockDto ToDto()
    {
        return new GenerateNewBlockDto()
        {
            Data =  Data
        };
    }
}