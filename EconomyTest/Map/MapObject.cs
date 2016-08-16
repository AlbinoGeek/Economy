﻿// <copyright file="MapObject.cs" company="Mewzor Holdings Inc.">
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
    /// must be set by the creator before simulating
    /// </summary>
    public Map parent = null;
    
    /// <summary>
    /// represents left position on map
    /// </summary>
    public int X;

    /// <summary>
    /// represents top position on map
    /// </summary>
    public int Y;

    public Color Colour = Color.Wheat;

    /// <summary>
    /// represents position on map
    /// </summary>
    public Vector2 Position
    {
        get
        {
            return new Vector2(this.X, this.Y);
        }
    }

    /// <summary>
    /// gets a list of all MapObjects near us
    /// </summary>
    /// <returns>list of nearby objects</returns>
    public IEnumerable<MapObject> GetNeighbors(float distance)
    {
        // Using Linq : Filter MapObject by Distance < 3
        return 
            from mapObject in parent.MapObjects
            where Vector2.Distance(mapObject.Position, Position) < distance
            select mapObject;
    }
    
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
