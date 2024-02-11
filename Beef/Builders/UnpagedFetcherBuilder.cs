using Beef.Core.Fetchers;
using Beef.Types;

namespace Beef.Builders; 

public class UnpagedFetcherBuilder<TRequest, TResponse>  : FetcherBuilder<TRequest, TResponse>
    where TResponse : new() {
    private readonly CoreUnpagedFetcher<TRequest, TResponse> _fetcher;

    protected override IBaseFetcher<TRequest, TResponse> Fetcher {
        get => _fetcher; 
        init => _fetcher = (value as CoreUnpagedFetcher<TRequest, TResponse>)!;
    }

    public UnpagedFetcherBuilder(HttpClient httpClient) : base(httpClient) { }

    protected override IBaseFetcher<TRequest, TResponse> MakeFetcher(HttpClient httpClient) =>
        new CoreUnpagedFetcher<TRequest, TResponse>(httpClient);
    
    
    public override FetcherBuilder<TRequest, TResponse> With(Func<TRequest, Task<object?>> valueGetter, StringEnum stringEnum) {
        _fetcher.RequestFieldGetters.Add(valueGetter);
        return this;
    }
}