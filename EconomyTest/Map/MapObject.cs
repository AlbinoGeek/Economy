// <copyright file="MapObject.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc. All rights reserved.
// </copyright>
/// <summary>
/// 
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
public class MapObject
{
    /// <summary>
    /// must be set by the creator before simulating
    /// </summary>
    public Map parent = null;

    public string Ascii = "■";

    /// <summary>
    /// represents left position on map
    /// </summary>
    public int X;

    /// <summary>
    /// represents top position on map
    /// </summary>
    public int Y;
}
