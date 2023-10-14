using GameOfLife.Interfaces;

namespace GameOfLife.Classes;

public class Tile
{
    public List<IEntity> Content { get; set; }
    public int XCoordinate { get; }
    public int YCoordinate { get; }
    public bool HasMouse { get; set; }
    public bool HasCat { get; set; }
    public bool HasScullion { get; set; }
    public Tile(int xCoordinate, int yCoordinate)
    {
        XCoordinate = xCoordinate;
        YCoordinate = yCoordinate;
        Content = new List<IEntity>(4);
    }
    public bool IsEntityLocated(string entity) => entity switch
    {
        "GameOfLife.Classes.Mouse" => HasMouse || Content.Exists(x => x.GetType().ToString() == entity),
        "GameOfLife.Classes.Cat" => HasCat || Content.Exists(x => x.GetType().ToString() == entity),
        "GameOfLife.Classes.Scullion" => HasScullion || Content.Exists(x => x.GetType().ToString() == entity),
        _ => Content.Exists(x => x.GetType().ToString() == entity)
    };
    public bool HasEntity(string entity) => Content.Exists(x => x.GetType().ToString() == entity);

    public void RestoreVariables()
    {
        HasMouse = false;
        HasCat = false;
        HasScullion = false;
    }
}