using System.Globalization;
using Beef.Types.Core.Responses;

namespace Beef.Core.Utils;

internal static class ResponseExtensions {
    public static IEnumerable<T>? ParseResponse<T>(this UnpagedResponse me) where T : new() {
        var props = typeof(T)
            .GetProperties()
            .Where(p => p is { CanRead: true, CanWrite: true })
            .ToDictionary(i => i.Name);
        var ret = new List<T>();
        foreach (var value in me.ValuesAsString) {
            var instance = new T();
            var columnsAndValues = me.Columns.Zip(value, (c, v) => (c.Name, v));
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
internal static class StringExtensions {
    internal static bool TryParse(this string me, out object ret, Type t) {
        ret = me;
        if (t == typeof(string))
            return true;
        if (t == typeof(DateTime)) {
            var b = DateTime.TryParse(me, out var date);
            ret = date;
            return b;
        }

        if (t == typeof(TimeSpan)) {
            var b = TimeSpan.TryParse(me, out var time);
            ret = time;
            return b;
        }
        if (t == typeof(decimal)) {
            var b = decimal.TryParse(me, NumberStyles.Any, CultureInfo.CreateSpecificCulture("en-us"), out var d);
            ret = d;
            return b;
        }
        if (t == typeof(int)) {
            var b = int.TryParse(me, out var i);
            ret = i;
            return b;
        }
        return false;
    } 
}

internal static class TypeExtensions {
    internal static bool IsBasic(this Type me) {
        return me.IsPrimitive || me == typeof(decimal) || me == typeof(string);
    }
}