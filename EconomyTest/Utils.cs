// <copyright file="Utils.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
using Console = Colorful.Console;

/// <summary>
/// collection of static helper methods using \ref Colorful.Console
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
public class Utils
{
    /// <summary>
    /// shows a message on the console of level DEBUG in \ref System.Drawing.Color.SteelBlue
    /// </summary>
    /// <param name="message">string to display</param>
    public static void LogDebug(string message)
    {
        Console.WriteLine($"[DEBUG] {message}", System.Drawing.Color.SteelBlue);
    }

    /// <summary>
    /// shows a message on the console of level INFO in \ref System.Drawing.Color.Wheat
    /// </summary>
    /// <param name="message">string to display</param>
    public static void LogInfo(string message)
    {
        Console.WriteLine($" [INFO] {message}", System.Drawing.Color.Wheat);
    }

    /// <summary>
    /// shows a message on the console of level WARN in \ref System.Drawing.Color.Yellow
    /// </summary>
    /// <param name="message">string to display</param>
    public static void LogWarn(string message)
    {
        Console.WriteLine($" [WARN] {message}", System.Drawing.Color.Yellow);
    }
}
