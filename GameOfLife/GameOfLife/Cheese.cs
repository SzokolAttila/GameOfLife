namespace GameOfLife;

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
    public int[] Position { get; }
    public void EndOfTurn()
    {
        FoodPoints++;
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

    public Cheese(int[] position)
    {
        Position = position;
        FoodPoints = 1;
    }
}