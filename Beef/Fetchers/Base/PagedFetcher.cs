using Beef.Builders;
using Beef.Types;

namespace Beef.Fetchers.Base;

public abstract class PagedFetcher<TRequest, TResponse> : Fetcher<TRequest, TResponse> {
    public abstract PagedEndpoints BaseUrl { get; }
    protected override FetcherBuilder<TRequest, TResponse> MakeBuilder(HttpClient httpClient) =>
        new PagedFetcherBuilder<TRequest, TResponse>(httpClient);

    public PagedFetcher(HttpClient httpClient) : base(httpClient) {
        Builder.For(BaseUrl);
    }
}