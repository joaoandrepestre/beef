using Beef.Core.Fetchers;
using Beef.Fetchers.Base;
using Beef.Types;
using Beef.Types.Responses; 

namespace Beef.Fetchers; 

public class CashProvisionFetcher : PagedFetcher<string, CashProvisionResponse>, IB3Fetcher<string, CashProvisionResponse> {
    private InstrumentFetcher _instrumentFetcher;
    public CashProvisionFetcher(HttpClient httpClient, InstrumentFetcher instrumentFetcher) : base(httpClient) {
        _instrumentFetcher = instrumentFetcher;
    }
    
    protected override IBaseFetcher<string, CashProvisionResponse> BuildFetcher() =>
        Builder
            .With(Task.FromResult<object?>, PagedRequestFieldSelector.Company)
            .With(async (string symbol) => {
                var searchResult = await _instrumentFetcher.Fetch(symbol);
                var instrument = searchResult?.FirstOrDefault();

                if (instrument is null)
                    return null;

                return instrument.TradingName;
            }, PagedRequestFieldSelector.TradingName)
            .Build();

    public override PagedEndpoints BaseUrl => PagedEndpoints.CashProvisions;
}