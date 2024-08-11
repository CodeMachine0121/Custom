using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;

namespace CustomBlockChainLab.Repositories;

public class ChainRepository: IChainRepository
{
    public BlockDomain GetBlockBy(int id)
    {
        return new BlockDomain
        {
            Id = id,
            Data = "mock-data",
            Hash = "mock-hash",
            PreviousHash = "mock-previous-hash",
            TimeStamp = DateTime.Now,
            Nonce = 0
        };
    }

    public void InsertBlock(BlockDomain newBlockDomain)
    {
        throw new NotImplementedException();
    }
}