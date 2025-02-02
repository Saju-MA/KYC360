namespace KYC360.Web.Utility;

public class Utility
{
    public static string ServicesAPIBase { get; set; } = "https://localhost:7001";
    public enum ApiType
    {
        GET, 
        POST, 
        PUT, 
        DELETE
    }
}
