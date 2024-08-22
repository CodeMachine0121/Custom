using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;

namespace CustomBlockChainLab.Repositories.Interfaces;

public interface IChainRepository
{
    Task<BlockDomain> GetBlockBy(int any);
    Task<BlockDomain> InsertBlock(BlockDomain newBlockDomain);
    Task<int> GetChainLength();
    Task<BlockDomain> UpdateBlock(EditBlockDto dto);
}