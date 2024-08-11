using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomBlockChainLab.Models.DataBases;

public class BlockchainDbContext(DbContextOptions<BlockchainDbContext> contextOptions): DbContext(contextOptions) 
{
    
    public DbSet<Block> Blocks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Block>().HasKey(x=> x.Id);
    }
}