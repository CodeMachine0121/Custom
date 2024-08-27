using System.Text.Json;
using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Services.Interfaces;
using RedisSDK;

namespace CustomBlockChainLab.Services.Caches;

public class ChainCacheService(IChainService chainService, RedisManager redisManager)
    : IChainService
{
    public async Task<BlockDomain> GetBlockById(int id)
    {
        return await redisManager.GetOrCreate($"block:{id}", async () => await chainService.GetBlockById(id));
    }

    public async Task<BlockDomain> GenerateNewBlock(GenerateNewBlockDto dto)
    {
        return await redisManager.GetOrCreate($"block:{(await chainService.GenerateNewBlock(dto)).Id}", async () => await chainService.GenerateNewBlock(dto));
    }

    public async Task<BlockDomain> EditBlock(EditBlockDto editBlockDto)
    {
        var blockDomain = await chainService.EditBlock(editBlockDto);
        redisManager.Update($"block:{blockDomain.Id}", blockDomain);
        return blockDomain;
    }
}