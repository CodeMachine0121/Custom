using EccSDK.models;
using EccSDK.models.ChameleonHash;

namespace CustomBlockChainLab.Models.Http;

public class GenerateNewBlockRequest
{
    public string Data { get; set; }

    public GenerateNewBlockDto ToDto(ChameleonSignature chameleonSignature, ChameleonHash chameleonHash)
    {
        return new GenerateNewBlockDto()
        {
            Data =  Data,
            ChameleonSignature = chameleonSignature,
            ChameleonHash = chameleonHash
        };
    }
}