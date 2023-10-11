using GameOfLife.Interfaces;

namespace GameOfLife.Classes;

public static class Grid
{
    public static int MaxWidth;
    public static int MaxHeight;
    public static int NumberOfScullions;
    public static int NumberOfCheeses;
    public static int NumberOfCats;
    public static int NumberOfMice;
    public static Tile[,] Map;

    public static List<Tile> AdjacentTiles(int x, int y)
    {
        var tiles = new List<Tile>();
        if (x > 0)
            tiles.Add(Map[y, x - 1]);
        if (y > 0)
            tiles.Add(Map[y - 1, x]);
        if (x < MaxHeight - 1)
            tiles.Add(Map[y, x + 1]);
        if (y < MaxWidth - 1)
            tiles.Add(Map[y + 1, x]);
        return tiles;
    } 
    
    public static List<Tile> AbleToStepOn(List<Tile> tiles, string entity)
    {
        return tiles.Where(x => !x.HasEntity(entity)).ToList();
    }
}