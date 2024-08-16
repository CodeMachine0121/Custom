using CustomBlockChainLab.Models.Domains;
using EccSDK;
using EccSDK.models;

namespace CustomBlockChainLab.Models;

public class GenerateNewBlockDto
{
    public string Data { get; set; }
    public DateTime TimeStamp { get; set; }
    public KeyPair KeyPair { get; set; }
    public SessionKey SessionKey { get; set; }

    public BlockDomain GetGenesisBlock()
    {
        return new BlockDomain
        {
            Data = "Genesis Block",
            Hash = "0",
            PreviousHash = "0",
            TimeStamp = DateTime.Now,
            Nonce = 0,
            ChameleonSignature = ChameleonHashHelper.Sign(new ChameleonHashRequest
            {
                KeyPair = KeyPair,
                Message = "Genesis Block",
                SessionKey = SessionKey.Key,
                Order = KeyPair.PublicKey.Curve.Order,
            })
        };
    }
}