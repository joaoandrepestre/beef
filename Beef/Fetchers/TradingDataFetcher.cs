using Beef.Builders;
using Beef.Core.Fetchers;
using Beef.Fetchers.Base;
using Beef.Types;
using Beef.Types.Requests;
using Beef.Types.Responses;

namespace Beef.Fetchers; 

public class TradingDataFetcher : UnpagedFetcher<SymbolDate, TradingDataResponse>, IB3Fetcher<SymbolDate, TradingDataResponse> {

    public TradingDataFetcher(HttpClient httpClient) : base(httpClient) {}
    protected override IBaseFetcher<SymbolDate, TradingDataResponse> BuildFetcher() =>
        Builder
        .With(symbolDate => Task.FromResult<object?>(symbolDate.Symbol))
        .With((symbolDate) => {
                var date = symbolDate.ReferenceDate;
                if (date == default)
                    return Task.FromResult<object?>(null);
                return Task.FromResult<object?>(date.ToString("yyyy-MM-dd"));
            })
            .Build();

    public override UnpagedEndpoints BaseUrl => UnpagedEndpoints.TradingData;
}