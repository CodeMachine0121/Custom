using CustomBlockChainLab.Models.Domains;

namespace CustomBlockChainLab.Services.Interfaces;

public interface IChainService
{
    Block GetBlockById(int i);
}