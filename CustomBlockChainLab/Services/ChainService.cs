using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Domains;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services.Interfaces;
using EccSDK.Models.ChameleonHash;
using EccSDK.models.Keys;
using EccSDK.Services.Interfaces;
using HashHelper = CustomBlockChainLab.Helpers.HashHelper;

namespace CustomBlockChainLab.Services;

public class ChainService(IChainRepository chainRepository, KeyPairDomain keyPairDomain, IChameleonHashService chameleonHashService) : IChainService
{
    private const int Nonce = 0;

    public async Task<BlockDomain> GetBlockById(int i)
    {
        return await chainRepository.GetBlockBy(i);
    }

    public async Task<BlockDomain> GenerateNewBlock(GenerateNewBlockDto dto)
    {
        var chainLength = await chainRepository.GetChainLength();
        var chameleonHash = chameleonHashService.CalculateChameleonHashBy(new ChameleonHashRequest()
        {
            KeyPairDomain = keyPairDomain
        });
        
        if (chainLength == 0)
        {
            var chameleonSignature = chameleonHashService.Sign("Genesis Block");
            var genesisBlock = new BlockDomain
            {
                Data = "Genesis Block",
                Hash = HashHelper.ToSha256($"{$"{dto.TimeStamp}:{"0"}:{Nonce}"}:{chameleonHash}") ,
                PreviousHash = "0",
                TimeStamp = DateTime.Now,
                Nonce = 0,
                ChameleonSignature = chameleonSignature
            };
            await chainRepository.InsertBlock(genesisBlock);
            return genesisBlock;
        }

        var latestBlockDomain = await chainRepository.GetBlockBy(chainLength);
        var nextBlock = new BlockDomain
        {
            Data = dto.Data,
            PreviousHash = latestBlockDomain.Hash,
            TimeStamp = dto.TimeStamp,
            Hash = HashHelper.ToSha256($"{$"{dto.TimeStamp}:{latestBlockDomain.Hash}:{Nonce}"}:{chameleonHash.Value}"),
            Nonce = Nonce,
            ChameleonSignature = chameleonHashService.Sign(dto.Data)
        };


        await chainRepository.InsertBlock(nextBlock);
        return nextBlock;
    }
}