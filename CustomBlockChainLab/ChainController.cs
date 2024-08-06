using CustomBlockChainLab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomBlockChainLab;

[Route("api/v1/[controller]")]
public class ChainController(IChainService chainService) : ControllerBase 
{
    public ApiResponse GetBlockById(int id)
    {
        var block = chainService.GetBlockById(id);
        return new ApiResponse()
        {
            Status = ResponseStatus.Ok
        };
    }
}

public class ApiResponse
{
    public ResponseStatus Status { get; set; }
}

public enum ResponseStatus
{
    Ok=0,
    Error=1
}