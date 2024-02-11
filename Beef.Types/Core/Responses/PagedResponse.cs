using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(nameof(Beef))]
namespace Beef.Types.Core.Responses; 

internal class PagedResponse<T> {
    public PageInfo Page { get; set; } = new();
    public IEnumerable<T> Results { get; set; } = Array.Empty<T>();
}

internal class PageInfo {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
}