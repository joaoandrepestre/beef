namespace Beef.Core.Types.Requests; 

internal class UnpagedRequest : IRequest {
    public string[] Params { get; set; }

    public string GetUrlParams() => string.Join("/", Params);
}