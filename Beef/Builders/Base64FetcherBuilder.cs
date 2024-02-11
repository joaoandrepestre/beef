using Beef.Core.Fetchers;
using Beef.Types;

namespace Beef.Builders; 

public class Base64FetcherBuilder<TRequest, TResponse> : FetcherBuilder<TRequest, TResponse> {
    private readonly CoreBase64Fetcher<TRequest, TResponse> _fetcher;

    protected override IBaseFetcher<TRequest, TResponse> Fetcher {
        get => _fetcher; 
        init => _fetcher = (value as CoreBase64Fetcher<TRequest, TResponse>)!;
    }
    public Base64FetcherBuilder(HttpClient httpClient) : base(httpClient){ }

    protected override IBaseFetcher<TRequest, TResponse> MakeFetcher(HttpClient httpClient) =>
        new CoreBase64Fetcher<TRequest, TResponse>(httpClient);

    public override FetcherBuilder<TRequest, TResponse> With(Func<TRequest, Task<object?>> valueGetter, StringEnum stringEnum) {
        _fetcher.LanguageGetter = valueGetter;
        return this;
    }
}