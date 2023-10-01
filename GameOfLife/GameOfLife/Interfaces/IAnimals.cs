namespace GameOfLife.Interfaces
{
    internal interface IAnimals : IEdible, IAlive
    {
        int TurnsLived { get; set; }
        int Eat();
        int MaxFoodPoints { get; set; }
        void Breed();
    }
}
