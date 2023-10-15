using GameOfLife.Interfaces;

namespace GameOfLife.Classes;

public class Scullion : IAlive
{
    private static readonly Random R = new();
    public static int MiceStunned = 0;
    public static int CatsFed = 0;
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
        if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
            MiceStunned++;
        if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Cat"))
            CatsFed++;
        DropCheese();
        Move();
    }

    public char Display => 'K';

    public void Move()
    {
        var tiles = Grid.AbleToStepOn(Grid.AdjacentTiles(_xCoordinate, _yCoordinate), "GameOfLife.Classes.Scullion");
        if (tiles.Count > 0)
        {
            var to = tiles[R.Next(tiles.Count)];
            to.HasScullion = true;
            XCoordinate = to.XCoordinate;
            YCoordinate = to.YCoordinate;
        }
    }

    private void DropCheese()
    {
        if (!Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Cheese"))
            Grid.Map[YCoordinate, XCoordinate].Content.Add(new Cheese(XCoordinate, YCoordinate));
    }
    public Scullion(int xPosition, int yPosition)
    {
        XCoordinate = xPosition;
        YCoordinate = yPosition;
    }
}