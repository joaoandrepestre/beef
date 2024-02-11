using Beef.Core.Fetchers;
using Beef.Fetchers.Base;
using Beef.Types;
using Beef.Types.Responses;

namespace Beef.Fetchers;

public class InstrumentFetcher : PagedFetcher<string, CompanySearchResponse>,
    IB3Fetcher<string, CompanySearchResponse> {

    public InstrumentFetcher(HttpClient httpClient) : base(httpClient) { }

    protected override IBaseFetcher<string, CompanySearchResponse> BuildFetcher() =>
        Builder
            .With(Task.FromResult<object?>, PagedRequestFieldSelector.Company)
            .Build();

    public override PagedEndpoints BaseUrl => PagedEndpoints.CompanySearch;
}
