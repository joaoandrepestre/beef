using Beef.Core.Types.Requests;

namespace Beef.Core.Fetchers; 

internal class CoreEmptyFetcher<TResponse> :
CoreFetcher<EmptyRequest, TResponse>, 
IBaseFetcher<TResponse> {
    public CoreEmptyFetcher(HttpClient httpClient) : base(httpClient) {}
    
    public async Task<IEnumerable<TResponse>> Fetch() {
        var req = EmptyRequest.Instance;
        return await Fetch(req);
    }
}