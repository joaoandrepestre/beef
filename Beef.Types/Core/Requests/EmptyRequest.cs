using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(nameof(Beef))]
namespace Beef.Types.Core.Requests; 

internal class EmptyRequest : IRequest {
    internal static readonly EmptyRequest Instance = new EmptyRequest();
    public string GetUrlParams() => "";
}