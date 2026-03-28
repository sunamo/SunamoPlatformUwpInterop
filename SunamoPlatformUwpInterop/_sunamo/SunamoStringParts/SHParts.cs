namespace SunamoPlatformUwpInterop._sunamo.SunamoStringParts;

/// <summary>
/// Provides string manipulation methods for extracting parts of strings.
/// </summary>
internal class SHParts
{
    /// <summary>
    /// Removes everything after the first occurrence of the specified separator.
    /// </summary>
    /// <param name="text">The source text to process.</param>
    /// <param name="separator">The separator to search for.</param>
    /// <returns>The text up to the first occurrence of the separator, or the original text if the separator is not found.</returns>
    internal static string RemoveAfterFirst(string text, string separator)
    {
        var index = text.IndexOf(separator);
        if (index == -1 || index == text.Length - 1) return text;

        var result = text.Remove(index);
        return result;
    }
}
