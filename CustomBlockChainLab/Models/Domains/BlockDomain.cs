using CustomBlockChainLab.Helpers;
using CustomBlockChainLab.Models.Entities;
using CustomBlockChainLab.Services;

namespace CustomBlockChainLab.Models.Domains;

public class BlockDomain
{
    public string Data { get; set; }
    public string Hash { get; set; }
    public string PreviousHash { get; set; }
    public DateTime TimeStamp { get; set; }
    public int Nonce { get; set; }

    public BlockDomain GenerateNextBlock(GenerateNewBlockDto dto, int nonce)
    {
        return new BlockDomain
        {
            Data = dto.Data,
            PreviousHash = Hash,
            TimeStamp = dto.TimeStamp,
            Hash = HashHelper.ToSha256($"{dto.TimeStamp}:{Hash}:{dto.Data}:{nonce}"),
            Nonce = nonce
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
            Nonce = Nonce
        };
    }
}