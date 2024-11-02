using System.Text.Json;

namespace Database.Services;

public class ApiBase
{
    public static string BaseUrl = "http://localhost:5064/";
    public static HttpClient Client = new HttpClient();
    public static JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };
}