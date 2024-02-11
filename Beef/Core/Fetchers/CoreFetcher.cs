using System.Text.Json;
using Beef.Core.Types.Requests;
using Beef.Core.Types.Responses;

namespace Beef.Core.Fetchers;
public interface IBaseFetcher<TRequest, TResponse> {
    Task<IEnumerable<TResponse>> Fetch(TRequest request);
    void SetBaseUrl(string baseUrl);
}

public interface IBaseFetcher<TResponse> {
    Task<IEnumerable<TResponse>> Fetch();
}
internal abstract class CoreFetcher<TRequest, TResponse> where TRequest : IRequest {
    private readonly HttpClient _httpClient;
    private static readonly string _http = "http://";
    private static readonly string _https = "https://";
    
    protected CoreFetcher(HttpClient httpClient) {
        _httpClient = httpClient;
    }
    
    internal string BaseUrl { get; set; }
    private string GetUrl(TRequest request) {
        var baseUrl = BaseUrl;
        if (!baseUrl.StartsWith(_http) && !baseUrl.StartsWith(_https))
            baseUrl = $"{_https}{baseUrl}";
        if (!baseUrl.EndsWith("/"))
            baseUrl += "/";
        return $"{baseUrl}{request.GetUrlParams()}";
    }

    private async Task<HttpResponseMessage?> DoHttpRequest(TRequest request) {
        var url = GetUrl(request);
        try {
            var res = await _httpClient.GetAsync(url);
            res.EnsureSuccessStatusCode();
            return res;
        }
        catch (Exception ex) {
            if (ex is InvalidCastException or HttpRequestException)
                return null;
            throw;
        }
    }

    private static async Task<T?> TryParseJson<T>(Stream stream) {
        try {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            return await JsonSerializer.DeserializeAsync<T>(stream, options);
        }
        catch (JsonException) {
            stream.Position = 0;
            return default;
        }
    }

    protected async Task<JsonResponse<TResponse>?> DoJsonHttpRequest(TRequest request) {
        var res = await DoHttpRequest(request);
        if (res is null) return null;
        var ret = new JsonResponse<TResponse>();
        var stream = await res.Content.ReadAsStreamAsync();
        ret.Value = await TryParseJson<TResponse>(stream);
        if (ret.Value is not null)
            return ret;
        ret.Values = await TryParseJson<IEnumerable<TResponse>>(stream);
        if (ret.Values is not null)
            return ret;
        return null;
    }

    protected async Task<IEnumerable<TResponse>> Fetch(TRequest? request) {
        if (request == null) return Enumerable.Empty<TResponse>();
        var jsonRes = await DoJsonHttpRequest(request);
        if (jsonRes is null)
            return Enumerable.Empty<TResponse>();
        if (jsonRes.Value is not null)
            return new[] { jsonRes.Value };
        if (jsonRes.Values is not null)
            return jsonRes.Values;
        return Enumerable.Empty<TResponse>();
    }
}

