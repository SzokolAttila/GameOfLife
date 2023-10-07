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
            MaxFoodPoints = 10;
            FoodPoints = 5;
            Speed = 1;
            XCoordinate = x;
            YCoordinate = y;
            Display = 'c';
            name = "";

        }

        public int TurnsLived { get;set; }
        public int MaxFoodPoints { get;set; }
        public int FoodPoints { get; set; }

        private int speed;
        public int Speed
        {
            get => speed;
            set
            {
                if (AdultKitten)
                {
                    speed = 2;
                }
                else
                {
                    speed = value;
                }
            }
        }
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
        public bool AdultKitten => TurnsLived >= 5;

        public List<Tile> AroundTiles(int xCoordinate, int yCoordinate)
        {
            List<Tile> list = new List<Tile>();
            for (int i = -1; i < 2; i++)
            {
                list.Add(Grid.Map[XCoordinate+i, YCoordinate + 1]);
            }
            for (int i = -1; i < 2; i++)
            {
                list.Add(Grid.Map[XCoordinate + i, YCoordinate - 1]);
            }
            list.Add(Grid.Map[XCoordinate - 1, YCoordinate]);
            list.Add(Grid.Map[XCoordinate + 1, YCoordinate]);
            return list;
        }
        public void NewKittenPutDown(List<Tile> availableTiles)
        {
            Tile nextTile = availableTiles[random.Next(availableTiles.Count)];
            int newKittenXCoordinate = nextTile.XCoordinate;
            int newKittenYCoordinate = nextTile.YCoordinate;
            Grid.Map[XCoordinate, YCoordinate].Content.Add(new Cat(newKittenXCoordinate, newKittenYCoordinate));
        }
        public void Breed()
        {
            if (AdultKitten && Grid.Map[XCoordinate,YCoordinate+1].HasEntity("cat"))
            {
                List<Tile> availableTiles = Grid.AbleToStepOn(AroundTiles(XCoordinate,YCoordinate), "cat");
                NewKittenPutDown(availableTiles);
            }
            else
            {
                if (AdultKitten && Grid.Map[XCoordinate + 1, YCoordinate].HasEntity("cat"))
                {
                    List<Tile> availableTiles = Grid.AbleToStepOn(AroundTiles(XCoordinate, YCoordinate), "cat");
                    NewKittenPutDown(availableTiles);
                }
                else
                {
                    if (AdultKitten && Grid.Map[XCoordinate, YCoordinate - 1].HasEntity("cat"))
                    {
                        List<Tile> availableTiles = Grid.AbleToStepOn(AroundTiles(XCoordinate, YCoordinate), "cat");
                        NewKittenPutDown(availableTiles);
                    }
                    else
                    {
                        if (AdultKitten && Grid.Map[XCoordinate-1, YCoordinate].HasEntity("cat"))
                        {
                            List<Tile> availableTiles = Grid.AbleToStepOn(AroundTiles(XCoordinate, YCoordinate), "cat");
                            NewKittenPutDown(availableTiles);
                        }
                    }
                }
            }
        }

        public void EndOfTurn()
        {
            FoodPoints--;
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
            Grid.Map[XCoordinate, YCoordinate].Content.Remove(this);
            List<Tile> availableTiles = Grid.AbleToStepOn(AroundTiles(XCoordinate, YCoordinate), "cat");
            Tile nextTile =  availableTiles[random.Next(availableTiles.Count)];
            XCoordinate = nextTile.XCoordinate;
            YCoordinate = nextTile.YCoordinate;
            Grid.Map[XCoordinate, YCoordinate].Content.Add(this);
        }

        public void Death()
        {
            Grid.Map[XCoordinate, YCoordinate].Content.Remove(this);
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
