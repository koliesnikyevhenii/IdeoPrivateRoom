namespace IdeoPrivateRoom.WebApi.Extension;

public static class StringExtensions
{
    public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue) where TEnum : struct, Enum
    {
        if (Enum.TryParse(value, true, out TEnum result))
        {
            return result;
        }

        return defaultValue;
    }
}
