using GameOfLife.Interfaces;

namespace GameOfLife.Classes;

public static class Grid
{
    public static int MaxWidth;
    public static int MaxHeight;
    public static Tile[,] Map = new Tile[MaxHeight, MaxWidth];
}