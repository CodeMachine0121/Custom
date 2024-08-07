using CustomBlockChainLab.Helpers;
using CustomBlockChainLab.Services;

namespace CustomBlockChainLab.Models.Domains;

public class Block
{
    public int Id { get; set; }
    public string Data { get; set; }
    public string Hash { get; set; }
    public string PreviousHash { get; set; }
    public DateTime TimeStamp { get; set; }
    public int Nonce { get; set; }

    public Block GenerateNextBlock(GenerateNewBlockDto dto, int nonce)
    {
        return new Block
        {
            Data = dto.Data,
            PreviousHash = Hash,
            TimeStamp = dto.TimeStamp,
            Hash = HashHelper.ToSha256($"{dto.TimeStamp}:{Hash}:{dto.Data}:{nonce}"),
            Nonce = nonce
        };
    }
}