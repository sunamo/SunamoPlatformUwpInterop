namespace SunamoPlatformUwpInterop._sunamo.SunamoExceptions;

/// <summary>
/// Provides exception message generation methods.
/// </summary>
internal sealed partial class Exceptions
{
    #region Other
    internal static string CheckBefore(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }

    internal static Tuple<string, string, string> PlaceOfException(
        bool isFillingAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var stackFrames = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        stackFrames.RemoveAt(0);
        string typeName = string.Empty;
        string methodName = string.Empty;
        for (var i = 0; i < stackFrames.Count; i++)
        {
            var currentFrame = stackFrames[i];
            if (isFillingAlsoFirstTwo)
                if (!currentFrame.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(currentFrame, out typeName, out methodName);
                    isFillingAlsoFirstTwo = false;
                }
            if (currentFrame.StartsWith("at System."))
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
        var qualifiedName = afterAt.Split("(")[0];
        var parts = qualifiedName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
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
    internal static string? IsNullOrWhitespace(string prefix, string argName, string argValue, bool isRejectingOnlyWhitespace)
    {
        string additionalParameters;
        if (argValue == null)
        {
            additionalParameters = AddParameters();
            return CheckBefore(prefix) + argName + " is null" + additionalParameters;
        }
        if (argValue == string.Empty)
        {
            additionalParameters = AddParameters();
            return CheckBefore(prefix) + argName + " is empty (without trim)" + additionalParameters;
        }
        if (isRejectingOnlyWhitespace && argValue.Trim() == string.Empty)
        {
            additionalParameters = AddParameters();
            return CheckBefore(prefix) + argName + " is empty (with trim)" + additionalParameters;
        }
        return null;
    }

    static readonly StringBuilder additionalInfoInnerStringBuilder = new();
    static readonly StringBuilder additionalInfoStringBuilder = new();

    internal static string AddParameters()
    {
        additionalInfoStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoStringBuilder.Insert(0, "Outer:");
        additionalInfoStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoInnerStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoInnerStringBuilder.Insert(0, "Inner:");
        additionalInfoInnerStringBuilder.Insert(0, Environment.NewLine);
        var additionalParameters = additionalInfoStringBuilder.ToString();
        var additionalParametersInner = additionalInfoInnerStringBuilder.ToString();
        return additionalParameters + additionalParametersInner;
    }
    #endregion

    #region OnlyReturnString
    internal static string? NotImplementedMethod(string prefix)
    {
        return CheckBefore(prefix) + "Not implemented method.";
    }
    #endregion

    internal static string WasAlreadyInitialized(string prefix)
    {
        return prefix + " was already initialized!";
    }

    internal static string? FirstLetterIsNotUpper(string prefix, string text)
    {
        return text.Length == 0 ? null :
        char.IsLower(text[0]) ? CheckBefore(prefix) + "First letter is not upper: " + text : null;
    }
}
