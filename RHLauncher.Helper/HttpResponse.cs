using Newtonsoft.Json;

namespace RHLauncher.RHLauncher.Helper;

public class HttpResponse
{
    [JsonProperty("success")]
    public string? Success { get; set; }

    [JsonProperty("result")]
    public string? Result { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }
}

public class LoginResponse
{
    [JsonProperty("result")]
    public string? Result { get; set; }

    [JsonProperty("token")]
    public string? Token { get; set; }

    [JsonProperty("windyCode")]
    public string? WindyCode { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }
}

public class ServerStatusResponse
{
    [JsonProperty("status")]
    public string? Status { get; set; }
}