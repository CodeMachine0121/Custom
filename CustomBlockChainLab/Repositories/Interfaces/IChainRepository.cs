using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;

namespace CustomBlockChainLab.Repositories.Interfaces;

public interface IChainRepository
{
    Task<BlockDomain> GetBlockBy(int any);
    Task InsertBlock(BlockDomain newBlockDomain);
    Task<int> GetChainLength();
    Task UpdateBlock(EditBlockDto dto);
}