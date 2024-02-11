using Beef.Builders;
using Beef.Types;

namespace Beef.Fetchers.Base; 

public abstract class Base64Fetcher<TRequest, TResponse> : Fetcher<TRequest, TResponse> {
    public abstract Base64Endpoints BaseUrl { get; }
    protected override FetcherBuilder<TRequest, TResponse> MakeBuilder(HttpClient httpClient) =>
        new Base64FetcherBuilder<TRequest, TResponse>(httpClient);

    public Base64Fetcher(HttpClient httpClient) : base(httpClient) {
        Builder.For(BaseUrl);
    }
}