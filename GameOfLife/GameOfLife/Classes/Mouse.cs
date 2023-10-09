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
            XCoordinate = x;
            YCoordinate = y;
            Display = 'e';
            Grid.Map[y, x].Content.Add(this);
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

        private int stunned = 0;
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
            stunned++;
        }

        public void Breed() // potential problem --> breeding twice
        {
            if (Grid.Map[YCoordinate - 1,XCoordinate].HasEntity("GameOfLife.Classes.Mouse") || Grid.Map[YCoordinate + 1,XCoordinate].HasEntity("GameOfLife.Classes.Mouse") ||
                Grid.Map[YCoordinate,XCoordinate - 1].HasEntity("GameOfLife.Classes.Mouse") || Grid.Map[YCoordinate - 1,XCoordinate + 1].HasEntity("GameOfLife.Classes.Mouse"))
            {
                if (!Grid.Map[YCoordinate - 1,XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
                {
                    new Mouse(XCoordinate, YCoordinate - 1);
                }
                else if (!Grid.Map[YCoordinate + 1,XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
                {
                    new Mouse(XCoordinate, YCoordinate + 1);
                }
                else if (!Grid.Map[YCoordinate,XCoordinate + 1].HasEntity("GameOfLife.Classes.Mouse"))
                {
                    new Mouse(XCoordinate + 1, YCoordinate);
                }
                else if (!Grid.Map[YCoordinate,XCoordinate - 1].HasEntity("GameOfLife.Classes.Mouse"))
                {
                    new Mouse(XCoordinate - 1, YCoordinate);
                }
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
                    if (Grid.Map[i,j].Content.Select(x=>x.Display).Contains('c')|| Grid.Map[i, j].Content.Select(x => x.Display).Contains('C'))
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
            for (int i = 0; i < Grid.Map.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.Map.GetLength(1); j++)
                {
                    if (Grid.Map[i,j].HasEntity("GameOfLife.Classes.Cheese"))
                    {
                        int Dist = Math.Abs(YCoordinate - i) + Math.Abs(XCoordinate - j) - (((Cheese)Grid.Map[YCoordinate, XCoordinate].
                            Content.Where(x => x.GetType().ToString() == "GameOfLife.Classes.Cheese").First()).FoodPoints - 1);
                        if (Dist < pos[2])
                        {
                            pos = new int[] { i, j, Dist };
                        }
                    }
                    else if (Grid.Map[i, j].Content.Select(x => x.Display).Contains('E'))
                    {
                        int Dist = Math.Abs(YCoordinate - i) + Math.Abs(XCoordinate - j) - 1;
                        if (Dist < pos[2])
                        {
                            pos = new int[] { i, j, Dist };
                        }
                    }
                    else if (Grid.Map[i, j].Content.Select(x => x.Display).Contains('P'))
                    {
                        int Dist = Math.Abs(YCoordinate - i) + Math.Abs(XCoordinate - j) - 2;
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
            if (cat[0] > YCoordinate && Grid.Map[YCoordinate - 1, XCoordinate].Content.Count() < 4)
            {
                YCoordinate--;
            }
            else if (cat[0] < YCoordinate && Grid.Map[YCoordinate + 1, XCoordinate].Content.Count() < 4)
            {
                YCoordinate++;
            }
            else if (cat[1] > XCoordinate && Grid.Map[YCoordinate, XCoordinate - 1].Content.Count() < 4)
            {
                XCoordinate--;
            }
            else if (Grid.Map[YCoordinate, XCoordinate + 1].Content.Count() < 4)
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
            if (cheese[0] > YCoordinate && !Grid.Map[YCoordinate + 1, XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
            {
                YCoordinate++;
            }
            else if (cheese[0] < YCoordinate && !Grid.Map[YCoordinate - 1, XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
            {
                YCoordinate--;
            }
            else if (cheese[1] > XCoordinate && !Grid.Map[YCoordinate, XCoordinate + 1].HasEntity("GameOfLife.Classes.Mouse"))
            {
                XCoordinate++;
            }
            else if (!Grid.Map[YCoordinate, XCoordinate - 1].HasEntity("GameOfLife.Classes.Mouse"))
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
            if (!Grid.Map[YCoordinate + 1, XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
            {
                YCoordinate++;
            }
            else if (!Grid.Map[YCoordinate - 1, XCoordinate].HasEntity("GameOfLife.Classes.Mouse"))
            {
                YCoordinate--;
            }
            else if (!Grid.Map[YCoordinate, XCoordinate + 1].HasEntity("GameOfLife.Classes.Mouse"))
            {
                XCoordinate++;
            }
            else if (!Grid.Map[YCoordinate, XCoordinate - 1].HasEntity("GameOfLife.Classes.Mouse"))
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
            if (stunned>0)
            {
                if (Grid.Map[YCoordinate, XCoordinate].HasEntity("GameOfLife.Classes.Cheese"))
                {
                    Eat(((Cheese)Grid.Map[YCoordinate, XCoordinate].Content.Where(x => x.GetType().ToString() == "GameOfLife.Classes.Cheese").First()).FoodPoints);
                }
                else
                {
                    Eat(0);
                }
                Breed();
                Move();
                stunned--;
            }
            if (Grid.Map[YCoordinate,XCoordinate].HasEntity("GameOfLife.Classes.Scullion"))
            {
                stunned++;
            }
            TurnsLived++;
        }

    }
}