
namespace Beef.Core.Types.Responses;

internal class JsonResponse<T> {
    public T? Value { get; set; }
    public IEnumerable<T>? Values { get; set; }
}