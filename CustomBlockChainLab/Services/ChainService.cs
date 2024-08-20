using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services.Interfaces;

namespace CustomBlockChainLab.Services;

public class ChainService(IChainRepository chainRepository) : IChainService
{
    public async Task<BlockDomain> GetBlockById(int id)
    {
        return await chainRepository.GetBlockBy(id);
    }

    public async Task<BlockDomain> GenerateNewBlock(GenerateNewBlockDto dto)
    {
        var chainLength = await chainRepository.GetChainLength();
        var previousHash = chainLength == 0
            ? ""
            : (await chainRepository.GetBlockBy(chainLength)).Hash;

        var nextBlock = dto.GetNextBlockDomain(previousHash);

        await chainRepository.InsertBlock(nextBlock);
        return nextBlock;
    }

    public Task EditBlock(EditBlockDto editBlockDto)
    {
       return Task.CompletedTask; 
    }
}