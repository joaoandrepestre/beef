using Beef.Core.Fetchers;
using Beef.Fetchers.Base;
using Beef.Types;
using Beef.Types.Responses;

namespace Beef.Fetchers; 

public class FinancialIndicatorFetcher : Base64Fetcher<string, FinancialIndicatorResponse>, IB3Fetcher<string, FinancialIndicatorResponse> {
    public FinancialIndicatorFetcher(HttpClient httpClient) : base(httpClient) { }
    public override Base64Endpoints BaseUrl => Base64Endpoints.FinancialIndicators;

    protected override IBaseFetcher<string, FinancialIndicatorResponse> BuildFetcher() =>
        Builder
            .With(lang => Task.FromResult<object?>(lang))
            .Build();

}