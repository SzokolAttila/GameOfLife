namespace GameOfLife.Interfaces
{
    public interface IEntity
    {
        int XCoordinate { get; }
        int YCoordinate { get; }
        void EndOfTurn();
        char Display { get; }
    }
}
