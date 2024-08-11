using CustomBlockChainLab.Models.DataBases;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Models.Entities;
using CustomBlockChainLab.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomBlockChainLab.Repositories;

public class ChainRepository(BlockchainDbContext blockchainDbContext): IChainRepository
{
    private DbSet<Block> _blocks = blockchainDbContext.Blocks;


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

    public async Task InsertBlock(BlockDomain newBlockDomain)
    {
        await _blocks.AddAsync(newBlockDomain.ToEntity());
        await blockchainDbContext.SaveChangesAsync();
    }

    public Task<int> GetChainLength()
    {
        return _blocks.CountAsync();
    }
}