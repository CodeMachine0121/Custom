using EccSDK.models;

namespace CustomBlockChainLab.Models.Http;

public class GenerateNewBlockRequest
{
    public string Data { get; set; }

    public GenerateNewBlockDto ToDto(KeyPair keyPair, SessionKey sessionKey)
    {
        return new GenerateNewBlockDto()
        {
            KeyPair = keyPair,
            SessionKey = sessionKey,
            Data =  Data
        };
    }
}