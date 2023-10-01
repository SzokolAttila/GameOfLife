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
    }

    public char Display => 'K';
    public int Speed { get; }

    public void Move()
    {
        for (var i = 0; i < Speed; i++)
        {
            XCoordinate += R.Next(-1, 1);
            YCoordinate += R.Next(-1, 1);
        }
    }

    public Scullion(int speed, int xPosition, int yPosition)
    {
        Speed = speed;
        XCoordinate = xPosition;
        YCoordinate = yPosition;
    }
}