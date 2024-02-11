namespace Beef.Core.Types.Requests; 

internal class PagedRequest : Base64Request, IRequest {
    private const string EmptyString = "aaaaa";
    public override string Language { get; set; } = "en-us";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 60;

    private string? _tradingName;
    public string TradingName {
        get => string.IsNullOrEmpty(_tradingName) ? EmptyString : _tradingName;
        set => _tradingName = value;
    }

    private string? _company;
    public string Company {
        get => string.IsNullOrEmpty(_company) ? EmptyString : _company;
        set => _company = value;
    }

    public override string ToString() =>
        $"{{\"language\":\"{Language}\",\"pageNumber\":{PageNumber},\"pageSize\":{PageSize},\"tradingName\":\"{TradingName}\",\"company\":\"{Company}\"}}";
}