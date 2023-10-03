﻿using GameOfLife.Interfaces;

namespace GameOfLife.Classes;

public static class Grid
{
    public static int MaxWidth;
    public static int MaxHeight;
    public static Tile[,] Map = new Tile[MaxHeight, MaxWidth];

    public static List<Tile> AdjacentTiles(int x, int y)
    {
        var tiles = new List<Tile>();
        if (x > 0)
            tiles.Add(Map[y, x - 1]);
        if (y > 0)
            tiles.Add(Map[y - 1, x]);
        if (x < MaxWidth - 1)
            tiles.Add(Map[y, x + 1]);
        if (y < MaxHeight - 1)
            tiles.Add(Map[y + 1, x]);
        return tiles;
    } 
    
    public static List<Tile> AbleToStepOn(List<Tile> tiles, string entity)
    {
        foreach (var tile in tiles.Where(tile => tile.HasEntity(entity)))
            tiles.Remove(tile);
        return tiles;
    }
}