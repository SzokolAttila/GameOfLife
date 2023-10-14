using GameOfLife.Interfaces;

namespace GameOfLife.Classes;

public class Cheese : IEdible
{
    public char Display
    {
        get
        {
            return FoodPoints switch
            {
                0 => 's',
                1 => 'S',
                2 => 'E',
                3 => 'P',
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    } 
    public int XCoordinate { get; }
    public int YCoordinate { get; }

    public void EndOfTurn()
    {
        FoodPoints++;
        if(Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
            Death();
    }

    private int _foodPoints;
    public int FoodPoints
    {
        get => _foodPoints;
        private set
        {
            if (value <= 3)
                _foodPoints = value;
        }
    }

    public void Death()
    {
        FoodPoints = 0;
    }

    public Cheese(int xCoordinate, int yCoordinate)
    {
        XCoordinate = xCoordinate;
        YCoordinate = yCoordinate;
        FoodPoints = 0;
    }
}