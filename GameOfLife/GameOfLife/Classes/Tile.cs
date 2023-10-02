using GameOfLife.Interfaces;

namespace GameOfLife.Classes;

public class Tile
{
    public List<IEntity> Content { get; set; }

    public Tile()
    {
        Content = new List<IEntity>(4);
    }

    public bool HasEntity(string entity) => Content.Exists(x => x.GetType().ToString() == entity);
}