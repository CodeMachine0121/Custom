using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Http;
using CustomBlockChainLab.Services.Interfaces;
using EccSDK;
using EccSDK.models;
using EccSDK.models.Keys;
using EccSDK.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomBlockChainLab;

[Route("api/v1/[controller]")]
public class ChainController(IChainService chainService, KeyPairDomain keyPairDomain, ChameleonHashService chameleonHashService) : ControllerBase 
{

    [HttpGet("{id}")]
    public async Task<ApiResponse> GetBlockById(int id)
    {
        var block = await chainService.GetBlockById(id);
        return ApiResponse.SuccessWithData(block);
    }

    [HttpPost("new")]
    public async Task<ApiResponse> GenerateNewBlock([FromBody] GenerateNewBlockRequest request)
    {
        var chameleonHash = chameleonHashService.GetChameleonHash();
        var chameleonSignature = chameleonHashService.Sign(request.Data);


        var newBlock = await chainService.GenerateNewBlock(request.ToDto(chameleonSignature, chameleonHash));
        return ApiResponse.SuccessWithData(newBlock);
    }
}