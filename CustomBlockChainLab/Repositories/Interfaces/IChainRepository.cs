using CustomBlockChainLab.Models.Domains;

namespace CustomBlockChainLab.Repositories.Interfaces;

public interface IChainRepository
{
    BlockDomain GetBlockBy(int any);
    Task InsertBlock(BlockDomain newBlockDomain);
}