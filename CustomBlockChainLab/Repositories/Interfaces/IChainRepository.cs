using CustomBlockChainLab.Models.Domains;

namespace CustomBlockChainLab.Repositories.Interfaces;

public interface IChainRepository
{
    Block GetBlockBy(int any);
}