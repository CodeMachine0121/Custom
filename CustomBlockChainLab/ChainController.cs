using CustomBlockChainLab.Models;
using CustomBlockChainLab.Models.Http;
using CustomBlockChainLab.Services.Interfaces;
using EccSDK;
using EccSDK.models;
using EccSDK.Models.ChameleonHash;
using EccSDK.Services;
using EccSDK.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomBlockChainLab;

[Route("api/v1/[controller]")]
public class ChainController(IChainService chainService, IChameleonHashService chameleonHashService) : ControllerBase 
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
        var chameleonHash = chameleonHashService.GetChameleonHash(request.Data);
        var chameleonSignature = chameleonHashService.Sign(request.Data);

        var newBlock = await chainService.GenerateNewBlock(request.ToDto(chameleonSignature, chameleonHash));
        return ApiResponse.SuccessWithData(newBlock);
    }
}