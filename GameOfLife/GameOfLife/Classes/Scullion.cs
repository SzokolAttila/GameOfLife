using GameOfLife.Interfaces;

namespace GameOfLife.Classes;

public class Scullion : IAlive
{
    private static readonly Random R = new();

    private int _xCoordinate;
    public int XCoordinate
    {
        get => _xCoordinate;
        private set
        {
            if (value < 0)
                _xCoordinate = 0;
            else if (value >= Grid.MaxWidth)
                _xCoordinate = Grid.MaxWidth - 1;
            else _xCoordinate = value;
        }
    }

    private int _yCoordinate;
    public int YCoordinate
    {
        get => _yCoordinate;
        private set
        {
            if (value < 0)
                _yCoordinate = 0;
            else if (value >= Grid.MaxHeight)
                _yCoordinate = Grid.MaxHeight - 1;
            else _yCoordinate = value;
        }
    }

    public void EndOfTurn()
    {
        Move();
        DropCheese();
    }

    public char Display => 'K';
    public int Speed { get; }

    public void Move()
    {
        var tiles = Grid.AbleToStepOn(Grid.AdjacentTiles(_xCoordinate, _yCoordinate), "GameOfLife.Classes.Scullion");
        var from = Grid.Map[_yCoordinate, _xCoordinate];
        var to = tiles[R.Next(0, tiles.Count)];
        from.Content.Remove(this);
        to.Content.Add(this);
        XCoordinate = to.XCoordinate;
        YCoordinate = to.YCoordinate;
    }

    private void DropCheese()
    {
        if (!Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Cheese"))
            Grid.Map[YCoordinate, XCoordinate].Content.Add(new Cheese(YCoordinate, XCoordinate));
    }
    public Scullion(int speed, int xPosition, int yPosition)
    {
        Speed = speed;
        XCoordinate = xPosition;
        YCoordinate = yPosition;
    }
}