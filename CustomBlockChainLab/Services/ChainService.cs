using CustomBlockChainLab.Helpers;
using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services.Interfaces;

namespace CustomBlockChainLab.Services;

public class ChainService(IChainRepository chainRepository) : IChainService
{
    private const int Nonce = 0;

    public BlockDomain GetBlockById(int i)
    {
        return chainRepository.GetBlockBy(i);
    }

    public BlockDomain GenerateNewBlock(GenerateNewBlockDto dto)
    {
        var firstBlock = chainRepository.GetBlockBy(0);
        var newBlock = firstBlock.GenerateNextBlock(dto, Nonce);
        
        chainRepository.InsertBlock(newBlock);
        return newBlock;
    }
}