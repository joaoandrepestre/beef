using Beef.Core.Types.Requests;

namespace Beef.Core.Fetchers; 

internal class CoreBase64Fetcher<TRequest, TResponse> :
    CoreFetcher<Base64Request, TResponse>,
    IBaseFetcher<TRequest, TResponse> {
    public CoreBase64Fetcher(HttpClient httpClient) : base(httpClient) { }
    
    internal Func<TRequest, Task<object?>>? LanguageGetter { get; set; }

    private async Task<string> GetLanguage(TRequest request) {
        var lang = "pt-br";
        if (LanguageGetter is null)
            return lang;
        var v = (await LanguageGetter(request))?.ToString();
        if (v is not null)
            lang = v;
        return lang;
    }
    private async Task<Base64Request?> GetBase64Request(TRequest request) {
        var lang = await GetLanguage(request);
        return new Base64Request {
            Language = lang,
        };
    }

    public async Task<IEnumerable<TResponse>> Fetch(TRequest request) {
        var req = await GetBase64Request(request);
        return await Fetch(req);
    }

    public void SetBaseUrl(string baseUrl) {
        BaseUrl = baseUrl;
    }
}