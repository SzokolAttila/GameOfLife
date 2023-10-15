using GameOfLife.Interfaces;


namespace GameOfLife.Classes
{
    internal class Cat : IAnimals
    {
        public static int Dead = 0;
        public static int Born = 0;
        public static List<Cat> DeadCats = new List<Cat>();
        private static List<string> ExistingNames = new List<string>();
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
            name = "";
            HadKitten = false;
        }
        public bool HadKitten { get; set; }
        public int TurnsLived { get;set; }
        private const int MaxFoodPoints = 8;
        public int FoodPoints { get; private set; }
        public int Speed => AdultKitten ? 2 : 1;

        public int XCoordinate { get; private set; }

        public int YCoordinate { get; private set; }

        public char Display => AdultKitten ? 'C' : 'c';

        private string name;
        public string Name
        {
            get
            {
                if (TurnsLived>=10 && name=="")
                {

                    int r = random.Next(0,catNames.Count);
                    string baseName = catNames[r];
                    string fullName = $"{ExistingNames.Where(x => x.Contains(baseName)).Count() + 1}. {baseName}";
                    name = fullName;
                    return fullName;
                }
                return name;
            }
        }
        private bool AdultKitten => TurnsLived >= 5;

        public void SpawnNewKitten(List<Tile> availableTiles)
        {
            Tile nextTile = availableTiles[random.Next(availableTiles.Count)];
            int newKittenXCoordinate = nextTile.XCoordinate;
            int newKittenYCoordinate = nextTile.YCoordinate;
            nextTile.HasCat = true;
            Grid.Map[YCoordinate, XCoordinate].Content.Add(new Cat(newKittenXCoordinate, newKittenYCoordinate));
        }

        private bool SearchForAdultCat(int xCoordinate, int yCoordinate)
        {
            int index = 0;
            while (index< Grid.Map[yCoordinate, xCoordinate].Content.
                Count(x => x.Display == 'c' || x.Display == 'C'))
            {
                var cat = (Cat)Grid.Map[yCoordinate, xCoordinate].Content.
                    Where(x => x.Display == 'c' || x.Display == 'C').ToList()[index];
                if (cat.AdultKitten && !cat.HadKitten && !HadKitten)
                {
                    cat.HadKitten = true;
                    return true;
                }
                index++;
            }
            return false;
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
                if (canBreed && Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), 
                    GetType().ToString()).Count > 0) 
                {
                    SpawnNewKitten(Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Cat"));
                    HadKitten = true;
                    Grid.NumberOfCats++;
                    Born++;
                }
            }
        }

        public void EndOfTurn()
        {
            HadKitten = false;
            if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
            {
                Mouse mouse = (Mouse)Grid.Map[YCoordinate, XCoordinate].Content.Find(x => x.GetType().ToString() == "GameOfLife.Classes.Mouse")!;
                Eat(mouse.FoodPoints/2);
            }
            if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Scullion"))
                Eat(1);

            FoodPoints--;
            if (FoodPoints == 0)
            {
                Death();
                return;
            }
            else
            {
                Breed();
                TurnsLived++;
                Move();
            }

            if (Name!="")
            {
                ExistingNames.Add(Name);
            }
        }

        public void Move()
        {
            List<Tile> availableTiles = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Cat");
            if (availableTiles.Count > 0)
            {
                Tile nextTile = availableTiles[random.Next(availableTiles.Count)];
                nextTile.HasCat = true;
                XCoordinate = nextTile.XCoordinate;
                YCoordinate = nextTile.YCoordinate;
            }
        }

        public void Death()
        {
            Grid.Map[YCoordinate, XCoordinate].Content.Remove(this);
            Grid.NumberOfCats--;
            Dead++;
            if (Name!="")
            {
                DeadCats.Add(this);
            }
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