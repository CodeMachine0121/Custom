using CustomBlockChainLab.Models;
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

    public async Task<BlockDomain> InsertBlock(BlockDomain newBlockDomain)
    {
        var entity = newBlockDomain.ToEntity();
        await _blocks.AddAsync(entity);
        await blockchainDbContext.SaveChangesAsync();
        return (await _blocks.FirstAsync(x=> x.Hash == entity.Hash)).ToDomain();
    }

    public async Task<int> GetChainLength()
    {
        return await _blocks.CountAsync();
    }

    public async Task<BlockDomain> UpdateBlock(EditBlockDto dto)
    {
        var blockToUpdate = await _blocks.FirstAsync(x=> x.Id == dto.Id);
        blockToUpdate.Data = dto.Data;
        blockToUpdate.ChameleonSignature = dto.ChameleonSignature;
        await blockchainDbContext.SaveChangesAsync();
        
        return blockToUpdate.ToDomain();
    }
}