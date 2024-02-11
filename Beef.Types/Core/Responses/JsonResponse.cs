using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(nameof(Beef))]
namespace Beef.Types.Core.Responses;

internal class JsonResponse<T> {
    public T? Value { get; set; }
    public IEnumerable<T>? Values { get; set; }
}