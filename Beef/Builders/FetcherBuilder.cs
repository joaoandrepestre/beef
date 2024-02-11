using Beef.Core.Fetchers;
using Beef.Types;

namespace Beef.Builders; 

public class FetcherBuilder<TResponse> {
    private readonly CoreEmptyFetcher<TResponse> _fetcher;

    public FetcherBuilder(HttpClient httpClient) {
        _fetcher = new CoreEmptyFetcher<TResponse>(httpClient);
    }

    public FetcherBuilder<TResponse> For(string baseUrl) {
        _fetcher.BaseUrl = baseUrl;
        return this;
    }

    public IBaseFetcher<TResponse> Build() => _fetcher;
}

public abstract class FetcherBuilder<TRequest, TResponse> {
    protected virtual IBaseFetcher<TRequest, TResponse> Fetcher { get; init; }
    protected abstract IBaseFetcher<TRequest, TResponse> MakeFetcher(HttpClient httpClient);

    public FetcherBuilder(HttpClient httpClient) {
        Fetcher = MakeFetcher(httpClient);
    }

    public FetcherBuilder<TRequest, TResponse> For(string baseUrl) {
        Fetcher.SetBaseUrl(baseUrl);
        return this;
    }

    public FetcherBuilder<TRequest, TResponse> With(Func<TRequest, Task<object?>> valueGetter) =>
        With(valueGetter, StringEnum.Empty);
    public abstract FetcherBuilder<TRequest, TResponse> With(Func<TRequest, Task<object?>> valueGetter, StringEnum stringEnum);
    public IBaseFetcher<TRequest, TResponse> Build() => Fetcher;
}