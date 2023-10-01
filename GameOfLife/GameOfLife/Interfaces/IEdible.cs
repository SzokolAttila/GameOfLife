namespace GameOfLife.Interfaces
{
    internal interface IEdible : IEntity
    {
        int FoodPoints { get; }
        void Death();
    }
}
