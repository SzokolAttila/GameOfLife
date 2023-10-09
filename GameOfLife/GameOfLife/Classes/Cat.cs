using GameOfLife.Interfaces;


namespace GameOfLife.Classes
{
    internal class Cat : IAnimals
    {
        private readonly List<string> _catNames = new List<string>
        {
            "Csöpi","Pötyi","Kormos","Hópihe","Maxi"
        };
        private static readonly Random Random = new Random();
        public Cat(int x, int y)
        {
            TurnsLived = 0;
            FoodPoints = 5;
            XCoordinate = x;
            YCoordinate = y;
            Display = 'c';
            _name = "";
        }

        public int TurnsLived { get; set; }
        private const int MaxFoodPoints = 10;
        public int FoodPoints { get; private set; }
        public int Speed => AdultKitten ? 2 : 1;
        public int XCoordinate { get; private set; }

        public int YCoordinate { get; private set; }

        public char Display { get; }

        private string _name;
        public string Name
        { 
            get=>_name;
            set
            {
                if (TurnsLived>=10)
                {
                    var r = Random.Next(0,_catNames.Count);
                    _name = _catNames[r];
                }
                else
                {
                    _name = value;
                }
            }
        }
        private bool AdultKitten => TurnsLived >= 5;

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
            Tile nextTile = availableTiles[Random.Next(availableTiles.Count)];
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
            Tile nextTile =  availableTiles[Random.Next(availableTiles.Count)];
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
