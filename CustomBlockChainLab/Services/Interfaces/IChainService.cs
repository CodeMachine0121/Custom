using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;

namespace CustomBlockChainLab.Services.Interfaces;

public interface IChainService
{
    BlockDomain GetBlockById(int i);
    BlockDomain GenerateNewBlock(GenerateNewBlockDto dto);
}