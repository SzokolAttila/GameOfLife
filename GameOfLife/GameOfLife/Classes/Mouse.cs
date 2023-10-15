using GameOfLife.Interfaces;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;

namespace GameOfLife.Classes
{
    internal class Mouse : IAnimals
    {
        private readonly static Random R = new Random();
        public static int Born = 0;
        public static int Dead = 0;
        public Mouse(int x, int y)
        {
            FoodPoints = 7;
            TurnsLived = 0;
            Speed = 1;
            _stunned = 0;
            XCoordinate = x;
            YCoordinate = y;
            Display = 'e';
            HadMouse = false;
        }

        public Mouse(int x, int y, int stun)
        {
            FoodPoints = 7;
            TurnsLived = 0;
            Speed = 1;
            _stunned = stun;
            XCoordinate = x;
            YCoordinate = y;
            Display = 'e';
            HadMouse = false;
        }

        public bool HadMouse { get; set; }
        private const int MaxFoodPoints = 7;
        private int _foodPoints;
        public int FoodPoints
        {
            get => _foodPoints;
            private set
            {
                if (value >= MaxFoodPoints)
                {
                    _foodPoints = MaxFoodPoints;
                }
                else if (value <= 0)
                {
                    Death();
                }
                else
                {
                    _foodPoints = value;
                }
            }
        }

        private int _stunned;
        public int TurnsLived { get; set; }
        public int Speed { get; }

        private int xCoordinate;
        public int XCoordinate
        {
            get => xCoordinate;
            set
            {
                if (value >= Grid.MaxWidth)
                {
                    xCoordinate = Grid.MaxWidth - 1;
                }
                else if (value <= 0)
                {
                    xCoordinate = 0;
                }
                else
                {
                    xCoordinate = value;
                }
            }
        }
        private int yCoordinate;
        public int YCoordinate
        {
            get => yCoordinate;
            set
            {
                if (value >= Grid.MaxHeight)
                {
                    yCoordinate = Grid.MaxHeight - 1;
                }
                else if (value <= 0)
                {
                    yCoordinate = 0;
                }
                else
                {
                    yCoordinate = value;
                }
            }
        }

        public char Display { get; init; }
        public bool IsStunned => _stunned > 0;
        private void Stun() => _stunned++;
        private bool CanBreed()
        {
            foreach (var tile in Grid.AdjacentTiles(XCoordinate, YCoordinate).Where(x => x.HasEntity(GetType().ToString())))
            {
                var mouse = (Mouse)tile.Content.Find(x => x.GetType().ToString() == GetType().ToString())!;
                if (!HadMouse && !mouse.HadMouse && !mouse.IsStunned)
                {
                    mouse.HadMouse = true;
                    return true;
                }
            }
            return false;
        }

        public void Breed() 
        {
            if (Grid.AdjacentTiles(XCoordinate, YCoordinate).Exists(x => x.HasEntity(GetType().ToString())))
            {
                List<Tile> available = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), GetType().ToString());
                if (CanBreed() && available.Count > 0)
                {
                    var to = available[R.Next(available.Count)];
                    to.Content.Add(new Mouse(to.XCoordinate, to.YCoordinate, 1));
                    to.HasMouse = true;
                    HadMouse = true;
                    Grid.NumberOfMice++;
                    Born++;
                }
            }
        }

        public void Death()
        {
            Grid.Map[YCoordinate,XCoordinate].Content.Remove(this);
            Grid.NumberOfMice--;
            Dead++;
        }

        public int Eat(int fp)
        {
            FoodPoints += fp;
            return FoodPoints;
        }

        private int[] ClosestCat()
        {
            int[] pos = new int[3];
            for (int i = 0; i < Grid.Map.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.Map.GetLength(1); j++)
                {
                    if (Grid.Map[i,j].HasEntity("GameOfLife.Classes.Cat"))
                    {
                        int Dist = Math.Abs(YCoordinate - i) + Math.Abs(XCoordinate - j);
                        if (Dist < pos[2])
                            pos = new int[] { i, j, Dist };
                    }
                }
            }
            return pos;
        }

        private int[] ClosestCheese()
        {
            int[] pos = new int[3];
            for (int i = 0; i < Grid.MaxHeight; i++)
            {
                for (int j = 0; j < Grid.MaxWidth; j++)
                {
                    if (Grid.Map[i, j].HasEntity("GameOfLife.Classes.Cheese"))
                    {
                        int Dist = Math.Abs(YCoordinate - i) + Math.Abs(XCoordinate - j) - (((Cheese)Grid.Map[i, j].
                            Content.Find(x => x.GetType().ToString() == "GameOfLife.Classes.Cheese")!).FoodPoints - 1);
                        if (Dist < pos[2])
                            pos = new int[] { i, j, Dist };
                    }
                }
            }
            return pos;
        }

        public void MoveFromCat(int[] cat)
        {
            List<Tile> available = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Mouse");
            if (cat[0] > YCoordinate && available.Exists(x => x.YCoordinate == this.YCoordinate - 1))
                PlaceToAnotherTile(0, -1);
            else if (cat[0] < YCoordinate && available.Exists(x => x.YCoordinate == this.YCoordinate + 1))
                PlaceToAnotherTile(0, 1);
            else if (cat[1] > XCoordinate && available.Exists(x => x.XCoordinate == this.XCoordinate - 1))
                PlaceToAnotherTile(-1, 0);
            else if (available.Exists(x => x.XCoordinate == this.XCoordinate + 1))
                PlaceToAnotherTile(1, 0);
            else
                DefMove();
        }

        public void MoveToCheese(int[] cheese)
        {
            List<Tile> available = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Mouse");
            if (cheese[0] > YCoordinate && available.Exists(x => x.YCoordinate == this.YCoordinate + 1))
                PlaceToAnotherTile(0, 1);
            else if (cheese[0] < YCoordinate && available.Exists(x => x.YCoordinate == this.YCoordinate - 1))
                PlaceToAnotherTile(0, -1);
            else if (cheese[1] > XCoordinate && available.Exists(x => x.XCoordinate == this.XCoordinate + 1))
                PlaceToAnotherTile(1, 0);
            else if (available.Exists(x => x.XCoordinate == this.XCoordinate - 1))
                PlaceToAnotherTile(-1, 0);
            else
                DefMove();
        }
        private void PlaceToAnotherTile(int x, int y)
        {
            var to = Grid.Map[YCoordinate + y, XCoordinate + x];
            to.HasMouse = true;
            XCoordinate = to.XCoordinate;
            YCoordinate = to.YCoordinate;
        }
        public void DefMove()
        {
            var tiles = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Mouse");
            if(tiles.Count > 0)
            {
                var to = tiles[R.Next(tiles.Count)];
                to.HasMouse = true;
                XCoordinate = to.XCoordinate;
                YCoordinate = to.YCoordinate;
            }
        }

        public void Move()
        {
            // public method which refreshes map by the positions?
            int[] cat = ClosestCat();
            int[] cheese = ClosestCheese();
            if (cat[2] > cheese[2])
                MoveToCheese(cheese);   
            else
                MoveFromCat(cat);
        }

        public void EndOfTurn()
        {
            HadMouse = false;   
            FoodPoints--;
            if (IsStunned)
                _stunned--;
            if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Scullion"))
                Stun();
            if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Cat"))
            {
                Death();
                return;
            }
            if (!IsStunned) 
            {
                if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Cheese"))
                    Eat(((Cheese)Grid.Map[YCoordinate, XCoordinate].Content.Find(x => x.GetType().ToString() == "GameOfLife.Classes.Cheese")!).FoodPoints);
                Breed();
                Move(); 
            }
            TurnsLived++;
        }

    }
}