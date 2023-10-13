using GameOfLife.Interfaces;
using System.Data;
using System.Linq;

namespace GameOfLife.Classes
{
    internal class Mouse : IAnimals
    {
        public Mouse(int x, int y)
        {
            FoodPoints = 3;
            TurnsLived = 0;
            Speed = 1;
            _stunned = 1;
            XCoordinate = x;
            YCoordinate = y;
            Display = 'e';
        }

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

        private void Stun()
        {
            _stunned++;
        }

        public void Breed() // potential problem --> breeding twice
        {
            var canBreed = false;
            foreach (var tile in Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Mouse"))
            {
                if (tile.HasEntity("GameOfLife.Classes.Mouse"))
                    canBreed = true;
            }
            if (canBreed)
            {
                List<Tile> available = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Mouse");
                new Mouse(available.First().XCoordinate, available.First().YCoordinate);
            }
        }

        public void Death()
        {
            Grid.Map[YCoordinate,XCoordinate].Content.Remove(this);
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
                        {
                            pos = new int[] { i, j, Dist };
                        }
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
                        {
                            pos = new int[] { i, j, Dist };
                        }
                    }
                }
            }
            return pos;
        }

        public void MoveFromCat(int[] cat)
        {
            List<Tile> available = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Mouse");
            if (cat[0] > YCoordinate && available.Where(x => x.YCoordinate == this.YCoordinate - 1).Count() > 0)
            {
                YCoordinate--;
            }
            else if (cat[0] < YCoordinate && available.Where(x => x.YCoordinate == this.YCoordinate + 1).Count() > 0)
            {
                YCoordinate++;
            }
            else if (cat[1] > XCoordinate && available.Where(x => x.XCoordinate == this.XCoordinate - 1).Count() > 0)
            {
                XCoordinate--;
            }
            else if (available.Where(x => x.XCoordinate == this.XCoordinate + 1).Count() > 0)
            {
                XCoordinate++;
            }
            else
            {
                DefMove();
            }
        }

        public void MoveToCheese(int[] cheese)
        {
            List<Tile> available = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Mouse");
            if (cheese[0] > YCoordinate && available.Where(x => x.YCoordinate == this.YCoordinate + 1).Count() > 0)
            {
                YCoordinate++;
            }
            else if (cheese[0] < YCoordinate && available.Where(x => x.YCoordinate == this.YCoordinate - 1).Count() > 0)
            {
                YCoordinate--;
            }
            else if (cheese[1] > XCoordinate && available.Where(x => x.XCoordinate == this.XCoordinate + 1).Count() > 0)
            {
                XCoordinate++;
            }
            else if (available.Where(x => x.YCoordinate == this.XCoordinate - 1).Count() > 0)
            {
                XCoordinate--;
            }
            else
            {
                DefMove();
            }
        }

        public void DefMove()
        {
            List<Tile> available = Grid.AbleToStepOn(Grid.AdjacentTiles(XCoordinate, YCoordinate), "GameOfLife.Classes.Mouse");
            if (available.Where(x => x.YCoordinate == this.YCoordinate + 1).Count() > 0)
            {
                YCoordinate++;
            }
            else if (available.Where(x => x.YCoordinate == this.YCoordinate - 1).Count() > 0)
            {
                YCoordinate--;
            }
            else if (available.Where(x => x.XCoordinate == this.XCoordinate + 1).Count() > 0)
            {
                XCoordinate++;
            }
            else if (available.Where(x => x.XCoordinate == this.XCoordinate - 1).Count() > 0)
            {
                XCoordinate--;
            }
        }

        public void Move()
        {
            // public method which refreshes map by the positions?
            int[] cat = ClosestCat();
            int[] cheese = ClosestCheese();
            if (cat[2] > cheese[2])
            {
                MoveToCheese(cheese);   
            }
            else
            {
                MoveFromCat(cat);
            }
        }

        public void EndOfTurn()
        {
            FoodPoints--;
            if (_stunned>0)
            {
                if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Cheese"))
                {
                    Eat(((Cheese)Grid.Map[YCoordinate, XCoordinate].Content.Find(x => x.GetType().ToString() == "GameOfLife.Classes.Cheese")!).FoodPoints);
                }
                Breed();
                Move();
                _stunned--;
            }
            if (Grid.Map[YCoordinate,XCoordinate].HasEntity("GameOfLife.Classes.Scullion"))
            {
                Stun();
            }
            TurnsLived++;
        }

    }
}