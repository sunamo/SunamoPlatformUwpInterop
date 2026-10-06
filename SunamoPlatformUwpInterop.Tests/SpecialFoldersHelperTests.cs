namespace SunamoPlatformUwpInterop.Tests;

/// <summary>
/// Tests for SpecialFoldersHelper (moved from sunamo.Tests.wpf desktop.Tests).
/// </summary>
public class SpecialFoldersHelperTests
{
    /// <summary>
    /// Verifies the ApplicationData folder path against a machine-specific user profile.
    /// </summary>
    [Fact(Skip = "Hard-coded machine-specific path C:/Users/n/AppData/ from the original test")]
    public void ApplicationData()
    {
        string expected = @"C:\Users\n\AppData\";
        string real = SpecialFoldersHelper.ApplicationData();
        Assert.Equal(expected, real);
    }
}
