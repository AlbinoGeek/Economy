using Console = Colorful.Console;

/// <summary>
/// 
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
public class Utils
{
    public static void LogDebug(string message)
    {
        Console.WriteLine("[DEBUG] " + message, System.Drawing.Color.SteelBlue);
    }

    public static void LogInfo(string message)
    {
        Console.WriteLine(" [INFO] " + message, System.Drawing.Color.Wheat);
    }

    public static void LogWarn(string message)
    {
        Console.WriteLine(" [WARN] " + message, System.Drawing.Color.Yellow);
    }
}
