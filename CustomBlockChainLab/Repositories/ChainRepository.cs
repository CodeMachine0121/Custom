using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;

namespace CustomBlockChainLab.Repositories;

public class ChainRepository: IChainRepository
{
    public Block GetBlockBy(int id)
    {
        return new Block
        {
            Id = id,
            Data = "mock-data",
            Hash = "mock-hash",
            PreviousHash = "mock-previous-hash",
            TimeStamp = DateTime.Now,
            Nonce = 0
        };
    }

    public void InsertBlock(Block newBlock)
    {
        throw new NotImplementedException();
    }
}