using System.Security.AccessControl;
using Beef.Core.Types;
using Beef.Core.Types.Requests;
using Beef.Core.Types.Responses;

namespace Beef.Core.Fetchers;

internal class CorePagedFetcher<TRequest, TResponse> :
    CoreFetcher<PagedRequest, PagedResponse<TResponse>>,
    IBaseFetcher<TRequest, TResponse> {

    internal CorePagedFetcher(HttpClient httpClient) : base(httpClient) {}

    internal readonly Dictionary<string, Func<TRequest, Task<object?>>> RequestFieldGetters = new();
    private async Task<PagedRequest?> GetPagedRequest(TRequest request, int pageNumber = 1) {
        var ret = new PagedRequest();
        var props = ret.GetType()
            .GetProperties()
            .Where(p => p is { CanRead: true, CanWrite: true});
        foreach (var prop in props) {
            if (RequestFieldGetters.TryGetValue(prop.Name, out var valueGetter)) {
                var value = await valueGetter(request);
                if (value is null) return null;
                if (value.GetType() != prop.PropertyType)
                    continue;
                prop.SetValue(ret, value);
            }
        }

        ret.PageNumber = pageNumber;
        return ret;
    }

    private async Task<PagedResponse<TResponse>?> GetPage(TRequest request, int pageNumber=1) {
        var req = await GetPagedRequest(request, pageNumber);
        return (await Fetch(req)).FirstOrDefault();
    }
    
    public async Task<IEnumerable<TResponse>> Fetch(TRequest request) {
        var pagedResponse = await GetPage(request);

        var totalPages = pagedResponse?.Page.TotalPages ?? 0;
        var ret = pagedResponse?.Results.ToList() ?? new List<TResponse>();
        for (var pageNumber = 2; pageNumber <= totalPages; pageNumber++) {
            pagedResponse = await GetPage(request, pageNumber);
            ret.AddRange(pagedResponse?.Results ?? Enumerable.Empty<TResponse>());
        }

        return ret;
    }

    public void SetBaseUrl(string baseUrl) {
        BaseUrl = baseUrl;
    }
}