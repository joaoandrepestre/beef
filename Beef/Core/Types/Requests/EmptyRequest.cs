namespace Beef.Core.Types.Requests; 

internal class EmptyRequest : IRequest {
    internal static readonly EmptyRequest Instance = new EmptyRequest();
    public string GetUrlParams() => "";
}