namespace CCANC.API.Common;

public class ApiResponse
{
    public int ResponseCode { get; set; }
    public string ResponseText { get; set; } = string.Empty;
    public object? Data { get; set; }
}