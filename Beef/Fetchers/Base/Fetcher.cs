using Beef.Builders;
using Beef.Core.Fetchers;
using Beef.Types;

namespace Beef.Fetchers.Base; 

public abstract class Fetcher<TResponse> {
    protected FetcherBuilder<TResponse> Builder;
    private IBaseFetcher<TResponse>? _fetcher = null;
    
    public abstract string BaseUrl { get; }

    public Fetcher(HttpClient httpClient) {
        Builder = new FetcherBuilder<TResponse>(httpClient).For(BaseUrl);
    }

    private IBaseFetcher<TResponse> BuildFetcher() => 
        Builder.Build();

    public Task<IEnumerable<TResponse>> Fetch() {
        if (_fetcher is null)
            _fetcher = BuildFetcher();
        return _fetcher.Fetch();
    }
}

public abstract class Fetcher<TRequest, TResponse> {
    protected FetcherBuilder<TRequest, TResponse> Builder;
    private IBaseFetcher<TRequest, TResponse>? _fetcher = null;
    

    protected abstract FetcherBuilder<TRequest, TResponse> MakeBuilder(HttpClient httpClient);
    public Fetcher(HttpClient httpClient) {
        Builder = MakeBuilder(httpClient);
    }

    protected abstract IBaseFetcher<TRequest, TResponse> BuildFetcher();

    public Task<IEnumerable<TResponse>> Fetch(TRequest request) {
        if (_fetcher is null)
            _fetcher = BuildFetcher();
        return _fetcher.Fetch(request);
    }
}

public abstract class UnsupportedFetcher<TRequest, TResponse> : Fetcher<TRequest, TResponse> {
    public abstract string BaseUrl { get; }

    protected UnsupportedFetcher(HttpClient httpClient) : base(httpClient) {
        Builder.For(BaseUrl);
    }
}