using Beef.Core.Utils;

namespace Beef.Core.Types.Responses; 

internal class UnpagedResponse {
    public string Name { get; set; }
    public IEnumerable<ColumnInfo> Columns { get; set; }
    public IEnumerable<IEnumerable<object>> Values { get; set; }
    public IEnumerable<IEnumerable<string>> ValuesAsString => Values
        .Select(i => i.Select(v => v?.ToString() ?? ""));

    public IEnumerable<T>? ParseResponse<T>() where T : new() {
        var props = typeof(T)
            .GetProperties()
            .Where(p => p is { CanRead: true, CanWrite: true })
            .ToDictionary(i => i.Name);
        var ret = new List<T>();
        foreach (var value in ValuesAsString) {
            var instance = new T();
            var columnsAndValues = Columns.Zip(value, (c, v) => (c.Name, v));
            foreach (var (c, v) in columnsAndValues)
            {
                if (!props.TryGetValue(c, out var prop)) continue;
                if (!v.TryParse(out var parsed, prop.PropertyType)) continue;
                prop.SetValue(instance, parsed);
            }
            ret.Add(instance);
        }
        return ret;
    }
}

internal class ColumnInfo {
    public string Name { get; set; }
    public string FriendlyName { get; set; }
    public string FriendlyNamePt { get; set; }
    public string FriendlyNameEn { get; set; }
    public int Type { get; set; }
    public int ColumnAlignment { get; set; }
    public int ValueAlignment { get; set; }
}