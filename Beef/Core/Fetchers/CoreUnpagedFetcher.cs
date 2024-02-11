using Beef.Core.Utils;
using Beef.Types.Core.Requests;
using Beef.Types.Core.Responses;

namespace Beef.Core.Fetchers; 


internal class CoreUnpagedFetcher<TRequest, TResponse> : 
    CoreFetcher<UnpagedRequest, UnpagedResponse>, 
    IBaseFetcher<TRequest, TResponse>
    
    where TResponse : new() {
    
    internal CoreUnpagedFetcher(HttpClient httpClient) : base(httpClient) { }
    internal readonly List<Func<TRequest, Task<object?>>> RequestFieldGetters = new();
    
    private async Task<UnpagedRequest?> GetUnpagedRequest(TRequest request) {
        var ret = new UnpagedRequest();
        var paramCount = RequestFieldGetters.Count;
        var parameters = new List<string>(paramCount);
        foreach (var valueGetter in RequestFieldGetters) {
            var value = await valueGetter(request);
            if (value is null) return null;
            var strValue = value.ToString();
            if (strValue is null) return null;
            parameters.Add(strValue);
        }

        ret.Params = parameters.ToArray();
        return ret;
    }

    public async Task<IEnumerable<TResponse>> Fetch(TRequest request) {
        var req = await GetUnpagedRequest(request);
        var response = (await Fetch(req)).FirstOrDefault();
        
        return response?.ParseResponse<TResponse>() ?? 
               Enumerable.Empty<TResponse>();
    }

    public void SetBaseUrl(string baseUrl) {
        BaseUrl = baseUrl;
    }
}