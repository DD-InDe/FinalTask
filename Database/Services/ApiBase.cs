using System.Text.Json;

namespace Database.Services;

public class ApiBase
{
    protected static readonly string BaseUrl = "http://localhost:5064/";
    protected static readonly HttpClient Client = new ();
    protected static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };
}