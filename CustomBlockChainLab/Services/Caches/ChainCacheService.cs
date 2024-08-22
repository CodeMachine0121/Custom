using System.Text.Json;
using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Services.Interfaces;
using StackExchange.Redis;

namespace CustomBlockChainLab.Services.Caches;

public class ChainCacheService(IConnectionMultiplexer redisRepository, IChainService chainService)
    : IChainService
{
    private readonly IDatabase _db = redisRepository.GetDatabase();

    public async Task<BlockDomain> GetBlockById(int id)
    {
        var cachedBlock = await _db.StringGetAsync($"block:{id}");

        if (!cachedBlock.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<BlockDomain>(cachedBlock!)!;
        }

        var blockDomain = await chainService.GetBlockById(id);
        await _db.StringSetAsync($"block:{id}", JsonSerializer.Serialize(blockDomain), TimeSpan.FromMinutes(5));
        return blockDomain;
    }

    public async Task<BlockDomain> GenerateNewBlock(GenerateNewBlockDto dto)
    {
        var generateNewBlock = await chainService.GenerateNewBlock(dto);
        await _db.StringSetAsync($"block:{generateNewBlock.Id}", JsonSerializer.Serialize(generateNewBlock));
        return generateNewBlock;
    }

    public async Task<BlockDomain> EditBlock(EditBlockDto editBlockDto)
    {
        var editBlock = await chainService.EditBlock(editBlockDto);
        await _db.StringSetAsync($"block:{editBlock.Id}", JsonSerializer.Serialize(editBlock));
        return editBlock;
    }
}