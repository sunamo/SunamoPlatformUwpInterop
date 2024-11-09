namespace SunamoPlatformUwpInterop._sunamo.SunamoExceptions;
internal partial class ThrowEx
{
    internal static bool FirstLetterIsNotUpper(string selectedFile)
    { return ThrowIsNotNull(Exceptions.FirstLetterIsNotUpper, selectedFile); }

    internal static bool IsNullOrEmpty(string argName, string argValue)
    { return ThrowIsNotNull(Exceptions.IsNullOrWhitespace(FullNameOfExecutedCode(), argName, argValue, true)); }
    internal static bool NotImplementedMethod() { return ThrowIsNotNull(Exceptions.NotImplementedMethod); }

    internal static bool WasAlreadyInitialized()
    { return ThrowIsNotNull(Exceptions.WasAlreadyInitialized(FullNameOfExecutedCode())); }

    #region Other
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfExc = Exceptions.PlaceOfException();
        string f = FullNameOfExecutedCode(placeOfExc.Item1, placeOfExc.Item2, true);
        return f;
    }

    static string FullNameOfExecutedCode(object type, string methodName, bool fromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (fromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type type2)
        {
            typeFullName = type2.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type t = type.GetType();
            typeFullName = t.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    internal static bool ThrowIsNotNull(string? exception, bool reallyThrow = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (reallyThrow)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }

    #region For avoid FullNameOfExecutedCode
    internal static bool ThrowIsNotNull(Exception exception, bool reallyThrow = true)
    {
        if (exception != null)
        {
            ThrowIsNotNull(exception.Message, reallyThrow);
            return false;
        }
        return true;
    }

    internal static bool ThrowIsNotNull<A, B>(Func<string, A, B, string?> f, A ex, B message)
    {
        string? exc = f(FullNameOfExecutedCode(), ex, message);
        return ThrowIsNotNull(exc);
    }

    internal static bool ThrowIsNotNull<A>(Func<string, A, string?> f, A ex)
    {
        string? exc = f(FullNameOfExecutedCode(), ex);
        return ThrowIsNotNull(exc);
    }

    internal static bool ThrowIsNotNull(Func<string, string?> f)
    {
        string? exc = f(FullNameOfExecutedCode());
        return ThrowIsNotNull(exc);
    }
    #endregion
    #endregion
}