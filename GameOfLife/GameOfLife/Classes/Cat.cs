using GameOfLife.Interfaces;


namespace GameOfLife.Classes
{
    internal class Cat : IAnimals
    {
        readonly List<string> catNames = new List<string>
        {
            "Csöpi","Pötyi","Kormos","Hópihe","Maxi"
        };
        static Random random = new Random();
        public Cat(int x, int y)
        {
            TurnsLived = 0;
            FoodPoints = 5;
            XCoordinate = x;
            YCoordinate = y;
            Display = 'c';
            name = "";
        }

        public int TurnsLived { get;set; }
        private const int MaxFoodPoints = 10;
        public int FoodPoints { get; private set; }
        public int Speed => AdultKitten ? 2 : 1;

        public int XCoordinate { get; private set; }

        public int YCoordinate { get; private set; }

        public char Display { get; init; }

        private string name;
        public string Name
        { 
            get=>name;
            set
            {
                if (TurnsLived>=10)
                {
                    int r = random.Next(0,catNames.Count);
                    name = catNames[r];
                }
                else
                {
                    name = value;
                }
            }
        }
        private bool AdultKitten => TurnsLived >= 5;

        public void SpawnNewKitten(List<Tile> availableTiles)
        {
            Tile nextTile = availableTiles[random.Next(availableTiles.Count)];
            int newKittenXCoordinate = nextTile.XCoordinate;
            int newKittenYCoordinate = nextTile.YCoordinate;
            Grid.Map[YCoordinate, XCoordinate].Content.Add(new Cat(newKittenXCoordinate, newKittenYCoordinate));
        }

        private bool SearchForAdultCat(int xCoordinate, int yCoordinate)
        {
            int index = 0;
            bool found = false;
            while (!found && index< Grid.Map[xCoordinate, yCoordinate].Content.Count)
            {
                if (Grid.Map[xCoordinate, yCoordinate].Content[index].Display==1)
                {
                    found = true;
                }
                index++;
            }
            return found;
        }
        public void Breed()
        {

            if (AdultKitten)
            {
                var canBreed = false;
                foreach (var tile in Grid.AdjacentTiles(XCoordinate, YCoordinate))
                {
                    if (tile.HasEntity("GameOfLife.Classes.Cat") && SearchForAdultCat(tile.XCoordinate, tile.YCoordinate))
                    {
                        canBreed = true;
                        break;
                    }
                }
                if (canBreed) 
                {
                    SpawnNewKitten(Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Cat"));
                    Grid.NumberOfCats++;
                }
            }
        }

        public void EndOfTurn()
        {
            FoodPoints--;
            if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
            {
                Mouse mouse = (Mouse)Grid.Map[YCoordinate, XCoordinate].Content.Find(x => x.GetType().ToString() == "GameOfLife.Classes.Mouse")!;
                Eat(mouse.FoodPoints/2);

            }
            if (Grid.Map[YCoordinate, YCoordinate].HasEntity("GameOfLife.Classes.Scullion"))
            {
                Eat(1);
            }


            if (FoodPoints == 0)
            {
                Death();
            }
            else
            {
                Breed();
                TurnsLived++;
                Move();
            }
        }

        public void Move()
        {
            Grid.Map[YCoordinate, XCoordinate].Content.Remove(this);
            List<Tile> availableTiles = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Cat");
            Tile nextTile =  availableTiles[random.Next(availableTiles.Count)];
            XCoordinate = nextTile.XCoordinate;
            YCoordinate = nextTile.YCoordinate;
            Grid.Map[YCoordinate, XCoordinate].Content.Add(this);
        }

        public void Death()
        {
            Grid.Map[YCoordinate, XCoordinate].Content.Remove(this);
            Grid.NumberOfCats--;
        }

        public int Eat(int foodPoints)
        {
            if (FoodPoints+foodPoints>MaxFoodPoints)
            {
                FoodPoints = MaxFoodPoints;
            }
            else
            {
                FoodPoints += foodPoints;
            }
            return FoodPoints;
            
        }
    }
}