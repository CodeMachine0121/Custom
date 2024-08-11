using CustomBlockChainLab.Models.DataBases;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Models.Entities;
using CustomBlockChainLab.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomBlockChainLab.Repositories;

public class ChainRepository(BlockchainDbContext blockchainDbContext): IChainRepository
{
    private DbSet<Block> _blocks = blockchainDbContext.Blocks;


    public async Task<BlockDomain> GetBlockBy(int id)
    {
        var block = await _blocks.FirstAsync(x=>x.Id == id);
        return block.ToDomain();
    }

    public async Task InsertBlock(BlockDomain newBlockDomain)
    {
        var entity = newBlockDomain.ToEntity();
        await _blocks.AddAsync(entity);
        await blockchainDbContext.SaveChangesAsync();
    }

    public async Task<int> GetChainLength()
    {
        return await _blocks.CountAsync();
    }
}