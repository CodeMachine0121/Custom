using CustomBlockChainLab.Helpers;
using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services.Interfaces;

namespace CustomBlockChainLab.Services;

public class ChainService(IChainRepository chainRepository) : IChainService
{
    private const int Nonce = 0;

    public Block GetBlockById(int i)
    {
        return chainRepository.GetBlockBy(i);
    }

    public Block GenerateNewBlock(GenerateNewBlockDto dto)
    {
        var firstBlock = chainRepository.GetBlockBy(0);

        var newBlock = new Block
        {
            Data = dto.Data,
            PreviousHash = firstBlock.Hash,
            TimeStamp = dto.TimeStamp,
            Hash = HashHelper.ToSha256($"{dto.TimeStamp}:{firstBlock.Hash}:{dto.Data}:{Nonce}"),
            Nonce = Nonce
        };
        
        chainRepository.InsertBlock(newBlock);
        
        return newBlock;
    }
}