using EccSDK.models;
using EccSDK.models.Keys;

namespace CustomBlockChainLab.Models.Http;

public class GenerateNewBlockRequest
{
    public string Data { get; set; }

    public GenerateNewBlockDto ToDto(KeyPairDomain keyPairDomain)
    {
        return new GenerateNewBlockDto()
        {
            KeyPairDomain = keyPairDomain,
            Data =  Data
        };
    }
}