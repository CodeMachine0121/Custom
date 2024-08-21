using CustomBlockChainLab.Models.Domains;

namespace CustomBlockChainLab.Models.Http;

public class ApiResponse
{
    public ResponseStatus Status { get; set; }
    public object Data { get; set; }

    public static ApiResponse SuccessWithData(object data)
    {
        return new ApiResponse()
        {
            Data = data,
            Status = ResponseStatus.Ok
        };
    }

    public static ApiResponse Success()
    {
        return new ApiResponse
        {
            Status = ResponseStatus.Ok,
            Data = "" 
        };
    }

    public static ApiResponse Fail()
    {
        return new ApiResponse()
        {
            Status = ResponseStatus.Error,
            Data = ""
        };
    }
}