using Beef.Builders;
using Beef.Types;

namespace Beef.Fetchers.Base; 

public abstract class UnpagedFetcher<TRequest, TResponse> : Fetcher<TRequest, TResponse>
    where TResponse : new() {
    public abstract UnpagedEndpoints BaseUrl { get; }
    protected override FetcherBuilder<TRequest, TResponse> MakeBuilder(HttpClient httpClient) =>
        new UnpagedFetcherBuilder<TRequest, TResponse>(httpClient);

    public UnpagedFetcher(HttpClient httpClient) : base(httpClient) {
        Builder.For(BaseUrl);
    }
}