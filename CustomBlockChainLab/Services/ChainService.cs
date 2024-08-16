using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services.Interfaces;
using EccSDK;
using EccSDK.models;

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
        
        var newBlock = chainLength == 0
            ? dto.GetGenesisBlock()
            : (await chainRepository.GetBlockBy(chainLength)).GenerateNextBlock(dto, Nonce);

        await chainRepository.InsertBlock(newBlock);
        return newBlock;
    }
}