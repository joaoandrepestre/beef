using System.Globalization;

namespace Beef.Core.Utils;

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