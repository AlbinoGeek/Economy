// <copyright file="MapObject.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Console = Colorful.Console;

/// <summary>
/// represents an object that can be drawn by \ref Map
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
public class MapObject
{
    /// <summary>
    /// represents left position on map
    /// </summary>
    public int X;

    /// <summary>
    /// represents top position on map
    /// </summary>
    public int Y;

    /// <summary>
    /// Gets coordinate on map
    /// </summary>
    public Vector2 Position
    {
        get
        {
            return new Vector2(this.X, this.Y);
        }
    }

    /// <summary>
    /// Gets or sets the world we are on
    /// </summary>
    internal Map Parent { get; set; }

    /// <summary>
    /// gets a list of all MapObjects near us
    /// </summary>
    /// <param name="distance">maximum radius to search</param>
    /// <returns>list of nearby objects</returns>
    public IEnumerable<MapObject> GetNeighbors(float distance)
    {
        // Using Linq : Filter MapObject by Distance < 3
        return 
            from mapObject in Parent.MapObjects
            where Vector2.Distance(mapObject.Position, Position) < distance
            select mapObject;
    }

    /// <summary>
    /// color used by \ref PrintColoured
    /// </summary>
    public Color Colour = Color.Wheat;

    /// <summary>
    /// prints a colored representation based on \ref ToAscii
    /// </summary>
    public void PrintColoured()
    {
        Console.Write(ToAscii(), Colour);
    }

    /// <summary>
    /// single character representation
    /// </summary>
    public virtual string ToAscii()
    {
        return "■";
    }
}
