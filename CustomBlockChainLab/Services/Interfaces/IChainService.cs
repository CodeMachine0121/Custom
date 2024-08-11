using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;

namespace CustomBlockChainLab.Services.Interfaces;

public interface IChainService
{
    Task<BlockDomain> GetBlockById(int i);
    Task<BlockDomain> GenerateNewBlock(GenerateNewBlockDto dto);
}