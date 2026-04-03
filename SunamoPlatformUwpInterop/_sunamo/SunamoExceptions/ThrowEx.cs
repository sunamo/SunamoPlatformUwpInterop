namespace SunamoPlatformUwpInterop._sunamo.SunamoExceptions;

/// <summary>
/// Provides methods for throwing exceptions with detailed context information.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws an exception if the first letter of the specified text is not uppercase.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True if an exception was thrown, false otherwise.</returns>
    internal static bool FirstLetterIsNotUpper(string text)
    { return ThrowIsNotNull(Exceptions.FirstLetterIsNotUpper, text); }

    /// <summary>
    /// Throws an exception if the specified argument value is null or empty.
    /// </summary>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="argValue">The value of the argument.</param>
    /// <returns>True if an exception was thrown, false otherwise.</returns>
    internal static bool IsNullOrEmpty(string argName, string argValue)
    { return ThrowIsNotNull(Exceptions.IsNullOrWhitespace(FullNameOfExecutedCode(), argName, argValue, true)); }

    /// <summary>
    /// Throws a not implemented method exception.
    /// </summary>
    /// <returns>True if an exception was thrown, false otherwise.</returns>
    internal static bool NotImplementedMethod() { return ThrowIsNotNull(Exceptions.NotImplementedMethod); }

    /// <summary>
    /// Throws an exception indicating that the object was already initialized.
    /// </summary>
    /// <returns>True if an exception was thrown, false otherwise.</returns>
    internal static bool WasAlreadyInitialized()
    { return ThrowIsNotNull(Exceptions.WasAlreadyInitialized(FullNameOfExecutedCode())); }

    #region Other
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    static string FullNameOfExecutedCode(object typeSource, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (typeSource is Type actualType)
        {
            typeFullName = actualType.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (typeSource is MethodBase methodBase)
        {
            typeFullName = methodBase.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = methodBase.Name;
        }
        else if (typeSource is string)
        {
            typeFullName = typeSource.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type objectType = typeSource.GetType();
            typeFullName = objectType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    internal static bool ThrowIsNotNull(string? exception, bool isReallyThrowing = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }

    #region For avoid FullNameOfExecutedCode

    internal static bool ThrowIsNotNull<TArgument>(Func<string, TArgument, string?> exceptionFactory, TArgument argument)
    {
        string? exception = exceptionFactory(FullNameOfExecutedCode(), argument);
        return ThrowIsNotNull(exception);
    }

    internal static bool ThrowIsNotNull(Func<string, string?> exceptionFactory)
    {
        string? exception = exceptionFactory(FullNameOfExecutedCode());
        return ThrowIsNotNull(exception);
    }
    #endregion
    #endregion
}
