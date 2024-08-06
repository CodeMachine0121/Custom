using CustomBlockChainLab.Models.Http;
using CustomBlockChainLab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomBlockChainLab;

[Route("api/v1/[controller]")]
public class ChainController(IChainService chainService) : ControllerBase 
{
    [HttpGet("{id}")]
    public ApiResponse GetBlockById(int id)
    {
        var block = chainService.GetBlockById(id);
        return ApiResponse.SuccessWithData(block);
    }
}