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

        return await chainRepository.InsertBlock(nextBlock);
    }

    public async Task<BlockDomain> EditBlock(EditBlockDto dto)
    {
        return await chainRepository.UpdateBlock(dto);
    }
}