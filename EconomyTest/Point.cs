// <copyright file="Point.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc. All rights reserved.
// </copyright>
using System;

/// <summary>
/// represents a coordinate on the map
/// </summary>
public struct Point
{
    public static float Distance(Point v1, Point v2)
    {
        return (float)Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2));
    }

    public int X { get; private set; }
    public int Y { get; private set; }

    public Point(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }
}
