namespace GameOfLife.Interfaces
{
    internal interface IAnimals : IEdible, IAlive
    {
        int TurnsLived { get; set; }
        int Eat(int foodPoints);
        private static int _maxFoodPoints;
        void Breed();
    }
}
