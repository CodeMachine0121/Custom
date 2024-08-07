using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services.Interfaces;

namespace CustomBlockChainLab.Services;

public class ChainService(IChainRepository chainRepository) : IChainService
{
    public Block GetBlockById(int i)
    {
        return chainRepository.GetBlockBy(i);
    }

    public Block GenerateNewBlock(GenerateNewBlockDto dto)
    {
        throw new NotImplementedException();
    }
}