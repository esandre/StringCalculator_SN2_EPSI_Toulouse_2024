namespace StringCalculator;

internal static class EraseSpacesExtensions
{
    public static string EraseSpaces(this string str)
    {
        return str.Replace(" ", "");
    }

    public static IEnumerable<string> EraseAllSpaces(this IEnumerable<string> str)
    {
        return str.Select(EraseSpaces);
    }
}