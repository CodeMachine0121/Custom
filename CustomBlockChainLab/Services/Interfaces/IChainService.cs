using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;

namespace CustomBlockChainLab.Services.Interfaces;

public interface IChainService
{
    Task<BlockDomain> GetBlockById(int id);
    Task<BlockDomain> GenerateNewBlock(GenerateNewBlockDto dto);
    Task<BlockDomain> EditBlock(EditBlockDto editBlockDto);
}