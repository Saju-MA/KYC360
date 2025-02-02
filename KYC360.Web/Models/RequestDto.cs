using static KYC360.Web.Utility.Utility;

namespace KYC360.Web.Models;

public class RequestDto
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
}
