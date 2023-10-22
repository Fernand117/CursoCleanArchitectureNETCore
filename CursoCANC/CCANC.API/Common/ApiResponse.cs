using CCANC.API.Common.ENUMS;

namespace CCANC.API.Common;

public class ApiResponse
{
    public EnumResponse ResponseCode { get; set; }
    public string ResponseText { get; set; } = string.Empty;
    public object? Data { get; set; }
}