using System.Collections.Frozen;

namespace Places.Core;

public static class EnumMapConverter
{
    public static FrozenDictionary<int, T> Enumerate<T>() where T : Enum =>
        ((T[])Enum.GetValues(typeof(T)))
        .ToDictionary(enumValue => Convert.ToInt32(enumValue))
        .ToFrozenDictionary();
}