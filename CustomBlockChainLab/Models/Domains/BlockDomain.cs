using CustomBlockChainLab.Models.Entities;
using EccSDK.models.ChameleonHash;
using EccSDK.Models.ChameleonHash;
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