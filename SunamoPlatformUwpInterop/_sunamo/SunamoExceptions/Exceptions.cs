namespace SunamoPlatformUwpInterop._sunamo.SunamoExceptions;

/// <summary>
/// Provides exception message generation methods.
/// </summary>
internal sealed partial class Exceptions
{
    #region Other
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    internal static Tuple<string, string, string> PlaceOfException(
        bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var stackFrames = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        stackFrames.RemoveAt(0);
        var i = 0;
        string typeName = string.Empty;
        string methodName = string.Empty;
        for (; i < stackFrames.Count; i++)
        {
            var item = stackFrames[i];
            if (isFillAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out typeName, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System."))
            {
                stackFrames.Add(string.Empty);
                stackFrames.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(typeName, methodName, string.Join(Environment.NewLine, stackFrames));
    }

    internal static void TypeAndMethodName(string stackFrame, out string typeName, out string methodName)
    {
        var afterAt = stackFrame.Split("at ")[1].Trim();
        var text = afterAt.Split("(")[0];
        var parts = text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        typeName = string.Join(".", parts);
    }

    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace
    internal static string? IsNullOrWhitespace(string before, string argName, string argValue, bool isNotAllowingOnlyWhitespace)
    {
        string additionalParams;
        if (argValue == null)
        {
            additionalParams = AddParams();
            return CheckBefore(before) + argName + " is null" + additionalParams;
        }
        if (argValue == string.Empty)
        {
            additionalParams = AddParams();
            return CheckBefore(before) + argName + " is empty (without trim)" + additionalParams;
        }
        if (isNotAllowingOnlyWhitespace && argValue.Trim() == string.Empty)
        {
            additionalParams = AddParams();
            return CheckBefore(before) + argName + " is empty (with trim)" + additionalParams;
        }
        return null;
    }

    static readonly StringBuilder additionalInfoInnerStringBuilder = new();
    static readonly StringBuilder additionalInfoStringBuilder = new();

    internal static string AddParams()
    {
        additionalInfoStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoStringBuilder.Insert(0, "Outer:");
        additionalInfoStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoInnerStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoInnerStringBuilder.Insert(0, "Inner:");
        additionalInfoInnerStringBuilder.Insert(0, Environment.NewLine);
        var additionalParams = additionalInfoStringBuilder.ToString();
        var additionalParamsInner = additionalInfoInnerStringBuilder.ToString();
        return additionalParams + additionalParamsInner;
    }
    #endregion

    #region OnlyReturnString
    internal static string? NotImplementedMethod(string before)
    {
        return CheckBefore(before) + "Not implemented method.";
    }
    #endregion

    internal static string WasAlreadyInitialized(string before)
    {
        return before + " was already initialized!";
    }

    internal static string? FirstLetterIsNotUpper(string before, string text)
    {
        return text.Length == 0 ? null :
        char.IsLower(text[0]) ? CheckBefore(before) + "First letter is not upper: " + text : null;
    }
}
