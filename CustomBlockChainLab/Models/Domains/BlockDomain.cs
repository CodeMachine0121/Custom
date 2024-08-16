using CustomBlockChainLab.Models.Entities;
using CustomBlockChainLab.Services;
using EccSDK;
using EccSDK.models;
using Org.BouncyCastle.Math;
using HashHelper = CustomBlockChainLab.Helpers.HashHelper;

namespace CustomBlockChainLab.Models.Domains;

public class BlockDomain
{
    public string Data { get; set; }
    public string Hash { get; set; }
    public string PreviousHash { get; set; }
    public DateTime TimeStamp { get; set; }
    public int Nonce { get; set; }

    public ChameleonSignature ChameleonSignature { get; set; }
    
    public BlockDomain GenerateNextBlock(GenerateNewBlockDto dto, int nonce)
    {
        return new BlockDomain
        {
            Data = dto.Data,
            PreviousHash = Hash,
            TimeStamp = dto.TimeStamp,
            Hash = HashHelper.ToSha256($"{dto.TimeStamp}:{Hash}:{dto.Data}:{nonce}"),
            Nonce = nonce,
            ChameleonSignature = ChameleonHashHelper.Sign(new ChameleonHashRequest
            {
                KeyPair = dto.KeyPair,
                Message = $"{dto.TimeStamp}:{Hash}:{dto.Data}:{nonce}",
                SessionKey = dto.SessionKey.Key,
                Order = dto.KeyPair.PublicKey.Curve.Order,
            }),
        };
    }



    public Block ToEntity()
    {
        return new Block
        {
            Data = Data,
            Hash = Hash,
            PreviousHash = PreviousHash,
            TimeStamp = TimeStamp,
            Nonce = Nonce,
            ChameleonSignature = ChameleonSignature.Value.ToString()
        };
    }
}