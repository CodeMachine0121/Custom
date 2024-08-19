using CustomBlockChainLab.Models.Domains;
using EccSDK.models.ChameleonHash;
using HashHelper = CustomBlockChainLab.Helpers.HashHelper;

namespace CustomBlockChainLab.Models;

public class GenerateNewBlockDto
{
    public string Data { get; set; }
    public DateTime TimeStamp { get; set; }
    public ChameleonSignature ChameleonSignature { get; set; }
    public ChameleonHash ChameleonHash { get; set; }

    public BlockDomain GetNextBlockDomain(string previousHash)
    {
        return new BlockDomain
        {
            Data = Data,
            PreviousHash = previousHash,
            TimeStamp = TimeStamp,
            Hash = HashHelper.ToSha256($"{TimeStamp}:{previousHash}:{0}:{ChameleonHash.Value}"),
            Nonce = 0,
            ChameleonSignature = ChameleonSignature
        };
    }
}