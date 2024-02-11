using System.Runtime.InteropServices;
using Beef.Core.Fetchers;
using Beef.Core.Types;
using Beef.Types;

namespace Beef.Builders;

public class PagedFetcherBuilder<TRequest, TResponse> : FetcherBuilder<TRequest, TResponse> {
    private readonly CorePagedFetcher<TRequest, TResponse> _fetcher;

    protected override IBaseFetcher<TRequest, TResponse> Fetcher {
        get => _fetcher; 
        init => _fetcher = (value as CorePagedFetcher<TRequest, TResponse>)!;
    }
    public PagedFetcherBuilder(HttpClient httpClient) : base(httpClient) { }

    protected override IBaseFetcher<TRequest, TResponse> MakeFetcher(HttpClient httpClient) =>
         new CorePagedFetcher<TRequest, TResponse>(httpClient);

    public override FetcherBuilder<TRequest, TResponse> With(Func<TRequest, Task<object?>> valueGetter, StringEnum key) {
        var selector = key as PagedRequestFieldSelector;
        if (selector is null)
            throw new ArgumentException($"Expected key to be {typeof(PagedRequestFieldSelector)}");
        return With(valueGetter, selector);
    }
    
    public PagedFetcherBuilder<TRequest, TResponse> With(Func<TRequest, Task<object?>> valueGetter, PagedRequestFieldSelector keyFieldSelector) {
        _fetcher.RequestFieldGetters.Add(keyFieldSelector.ToString(), valueGetter);
        return this;
    }
}