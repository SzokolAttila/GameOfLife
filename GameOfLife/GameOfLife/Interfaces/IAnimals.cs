namespace GameOfLife.Interfaces
{
    internal interface IAnimals : IEdible, IAlive
    {
        int TurnsLived { get; set; }
        int Speed { get; }
        int Eat(int foodPoints);
        void Breed();
    }
}
