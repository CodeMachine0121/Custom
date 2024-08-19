using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services.Interfaces;

namespace CustomBlockChainLab.Services;

public class ChainService(IChainRepository chainRepository) : IChainService
{
    private const int Nonce = 0;

    public async Task<BlockDomain> GetBlockById(int i)
    {
        return await chainRepository.GetBlockBy(i);
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
}