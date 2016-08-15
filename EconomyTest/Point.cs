// <copyright file="Point.cs" company="Mewzor Holdings Inc.">
//     Copyright (c) Mewzor Holdings Inc. All rights reserved.
// </copyright>
/// <summary>
/// represents a coordinate on the map
/// </summary>
public struct Point
{
    public int X;
    public int Y;

    public Point(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public float Distance(Point other)
    {
        return (other.X - X) + (other.Y - Y);
    }
}
