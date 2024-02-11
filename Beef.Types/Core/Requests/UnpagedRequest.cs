using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(nameof(Beef))]
namespace Beef.Types.Core.Requests; 

internal class UnpagedRequest : IRequest {
    public string[] Params { get; set; }

    public string GetUrlParams() => string.Join("/", Params);
}