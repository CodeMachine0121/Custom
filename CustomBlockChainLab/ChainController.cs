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
    
    [HttpPatch("edit/{id}")]
    public async Task<ApiResponse> EditBlock([FromBody] EditBlockRequest request, int id)
    {
        var chameleonSignature = chameleonHashService.Sign(request.Data);
        await chainService.EditBlock(new EditBlockDto
        {
            Id = id,
            Data = request.Data,
            ChameleonSignature = chameleonSignature.Value
        });
        
        return ApiResponse.Success();
    }
}