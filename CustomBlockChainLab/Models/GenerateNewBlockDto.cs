using CustomBlockChainLab.Models.Domains;
using EccSDK;
using EccSDK.models;
using EccSDK.models.Keys;

namespace CustomBlockChainLab.Models;

public class GenerateNewBlockDto
{
    public string Data { get; set; }
    public DateTime TimeStamp { get; set; }
    public KeyPairDomain KeyPairDomain { get; set; }
}