// <copyright file="Map.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc.  No Rights Reserved.
//     Licensed under the "Do What the Fuck You Want To Public License"
// </copyright>
using System;
using System.Collections.Generic;

/// <summary>
/// represents a console graphic \see this.Display
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
public class Map
{
    /// <summary>
    /// console horizontal length of map
    /// </summary>
    public int Width;

    /// <summary>
    /// console vertical length of map
    /// </summary>
    public int Height;

    // TODO(Albino) This musn't be public

    /// <summary>
    /// actual map data
    /// </summary>
    public MapTile[][] map;

    // TODO(Albino) This probably doesn't have to be public

    /// <summary>
    /// objects we draw on the map \see this.Display
    /// </summary>
    public List<MapObject> MapObjects;

    /// <summary>
    /// Initializes a new instance of the <see cref="Map" /> class. with dimensions
    /// </summary>
    /// <param name="width">horizontal length to create</param>
    /// <param name="height">vertical length to create</param>
    public Map(int width, int height)
    {
        this.Width = width;
        this.Height = height;

        this.MapObjects = new List<MapObject>();
        this.map = new MapTile[this.Width][];
        for (int x = 0; x < this.map.Length; ++x)
        {
            this.map[x] = new MapTile[this.Height];
        }
    }
    
    /// <summary>
    /// draw a representation of \ref this.map on the console
    /// </summary>
    /// <param name="yOffset">vertical length to offset drawing by</param>
    public void Display(int yOffset = 0)
    {
        if (yOffset == 0)
        {
            Console.Clear();
        }

        // Draw the map's tiles
        for (int x = 0; x < this.map.Length; ++x)
        {
            for (int y = 0; y < this.map[x].Length + 0; ++y)
            {
                Console.SetCursorPosition(x, y + yOffset);
                Console.Write(this.ToAscii(this.map[x][y]));
            }
        }

        // Draw people on top of the map
        for (int i = 0; i < this.MapObjects.Count; i++)
        {
            Console.SetCursorPosition(this.MapObjects[i].X, this.MapObjects[i].Y + yOffset);
            Console.Write(this.MapObjects[i].Ascii);
        }

        Console.SetCursorPosition(0, this.Height + yOffset);
    }

    /// <summary>
    /// fills \ref this.map with procedural data
    /// </summary>
    public void Generate()
    {
        this.DrawBorder();
    }

    /// <summary>
    /// adds to the list of objects we draw
    /// </summary>
    /// <param name="mapObject">object to add</param>
    public void Register(MapObject mapObject)
    {
        mapObject.parent = this;
        this.MapObjects.Add(mapObject);
    }

    /// <summary>
    /// single character representation of a specific tile
    /// </summary>
    /// <param name="tile">tile to represent</param>
    /// <returns>single character representation</returns>
    public string ToAscii(MapTile tile)
    {
        if (tile == MapTile.Wall)
        {
            return "█";
        }
        else if (tile == MapTile.Ground)
        {
            return "▒";
        }

        return "░";
    }

    /// <summary>
    /// adds a border of walls around \ref this.map
    /// </summary>
    private void DrawBorder()
    {
        // Draw horizontal borders
        this.DrawLine(0, 0, this.Width);
        this.DrawLine(0, this.Height - 1, this.Width);

        // Draw vertical borders
        this.DrawLine(0, 0, this.Height, true);
        this.DrawLine(this.Width - 1, 0, this.Height, true);
    }

    // TODO Why do it this way?

    /// <summary>
    /// adds walls in a horizontal line
    /// </summary>
    /// <param name="x">starting horizontal position</param>
    /// <param name="y">starting vertical position</param>
    /// <param name="length">horizontal length to draw</param>
    private void DrawLine(int x, int y, int length)
    {
        for (int i = x; i < length + x; i++)
        {
            this.map[i][y] = MapTile.Wall;
        }
    }

    // TODO Why do it this way?
    // HACK: Using extra argument for overloading

    /// <summary>
    /// adds walls in a vertical line
    /// </summary>
    /// <param name="x">starting horizontal position</param>
    /// <param name="y">starting vertical position</param>
    /// <param name="length">vertical length to draw</param>
    /// <param name="vertical">used to create overload</param>
    private void DrawLine(int x, int y, int length, bool vertical)
    {
        for (int i = y; i < length + y; i++)
        {
            this.map[x][i] = MapTile.Wall;
        }
    }
}
