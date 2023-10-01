namespace GameOfLife.Interfaces
{
    internal interface IAlive : IEntity
    {
        int Speed { get; }
        void Move();
    }
}
