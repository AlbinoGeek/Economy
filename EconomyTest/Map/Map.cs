using System;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.  We want to have public methods.")]
public class Map
{
    public int Width;
    public int Height;

    // TODO(Albino) This musn't be public
    public MapTile[][] map;

    // TODO(Albino) This probably doesn't have to be public
    public List<MapObject> MapObjects;

    public Map(int Width, int Height)
    {
        this.Width = Width;
        this.Height = Height;

        this.MapObjects = new List<MapObject>();
        map = new MapTile[Width][];
        for (int x = 0; x < map.Length; ++x)
        {
            map[x] = new MapTile[Height];
        }
    }
    
    public void Display(int yOffset = 0)
    {
        if (yOffset == 0)
        {
            Console.Clear();
        }

        // Draw the map's tiles
        for (int x = 0; x < map.Length; ++x)
        {
            for (int y = 0; y < map[x].Length + 0; ++y)
            {
                Console.SetCursorPosition(x, y + yOffset);
                Console.Write(ToAscii(map[x][y]));
            }
        }

        // Draw people on top of the map
        for (int i = 0; i < MapObjects.Count; i++)
        {
            Console.SetCursorPosition(MapObjects[i].X, MapObjects[i].Y + yOffset);
            Console.Write(MapObjects[i].Ascii);
        }

        Console.SetCursorPosition(0, Height + yOffset);
    }

    public void Generate()
    {
        drawBorder();
    }

    /// <summary>
    /// adds to the list of objects we draw
    /// </summary>
    /// <param name="mapObject">object to add</param>
    public void Register(MapObject mapObject)
    {
        mapObject.parent = this;
        MapObjects.Add(mapObject);
    }

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

    private void drawBorder()
    {
        // Draw horizontal borders
        drawLine(0, 0, Width-1);
        drawLine(0, Height-1, Width-1);
        
        // Draw vertical borders
        drawLine(0, 0, Height, true);
        drawLine(Width-1, 0, Height, true);
    }

    // TODO Why do it this way?
    private void drawLine(int x, int y, int length)
    {
        for (int i = x; i < length + x; i++)
        {
            map[i][y] = MapTile.Wall;
        }
    }

    // TODO Why do it this way?
    private void drawLine(int x, int y, int length, bool vertical)
    {
        for (int i = y; i < length + y; i++)
        {
            map[x][i] = MapTile.Wall;
        }
    }
}
