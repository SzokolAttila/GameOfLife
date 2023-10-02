using GameOfLife.Interfaces;

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
            Grid.Map[y, x][x] = this;
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

        public void Breed() // potential problem --> breeding twice
        {
            if (Map[YCoordinate - 1][XCoordinate].Display == 'e' || Map[YCoordinate + 1][XCoordinate].Display == 'e' ||
                Map[YCoordinate][XCoordinate - 1].Display == 'e' || Map[YCoordinate - 1][XCoordinate + 1].Display == 'e')
            {
                if (Map[YCoordinate - 1][XCoordinate] == null)
                {
                    new Mouse(XCoordinate, YCoordinate - 1);
                }
                else if (Map[YCoordinate + 1][XCoordinate] == null)
                {
                    new Mouse(XCoordinate, YCoordinate + 1);
                }
                else if (Map[YCoordinate][XCoordinate + 1] == null)
                {
                    new Mouse(XCoordinate + 1, YCoordinate);
                }
                else if (Map[YCoordinate][XCoordinate - 1] == null)
                {
                    new Mouse(XCoordinate - 1, YCoordinate);
                }
            }
        }

        public void Death()
        {
            Map[YCoordinate].RemoveAt(XCoordinate);
        }

        public int Eat(int foodPoints)
        {
            if (Map[YCoordinate][XCoordinate].Display == 'S')
            {
                ((IEdible)Map[YCoordinate][XCoordinate]).Death();
                return 1;
            }
            else if (Map[YCoordinate][XCoordinate].Display == 'E')
            {
                ((IEdible)Map[YCoordinate][XCoordinate]).Death();
                return 2;
            }
            else if (Map[YCoordinate][XCoordinate].Display == 'P')
            {
                ((IEdible)Map[YCoordinate][XCoordinate]).Death();
                return 3;
            }
            return 0;
        }

        private int[] ClosestCat()
        {
            int[] pos = new int[3];
            for (int i = 0; i < Map.Count(); i++)
            {
                for (int j = 0; j < Map[0].Count(); j++)
                {
                    if (Map[i][j].Display == 'c' || Map[i][j].Display == 'C')
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
            for (int i = 0; i < Map.Count(); i++)
            {
                for (int j = 0; j < Map[0].Count(); j++)
                {
                    if (Map[i][j].Display == 'S')
                    {
                        int Dist = Math.Abs(YCoordinate - i) + Math.Abs(XCoordinate - j);
                        if (Dist < pos[2])
                        {
                            pos = new int[] { i, j, Dist };
                        }
                    }
                    else if (Map[i][j].Display == 'E')
                    {
                        int Dist = Math.Abs(YCoordinate - i) + Math.Abs(XCoordinate - j) - 1;
                        if (Dist < pos[2])
                        {
                            pos = new int[] { i, j, Dist };
                        }
                    }
                    else if (Map[i][j].Display == 'P')
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

        public void Move()
        {
            // public method which refreshes map by the positions?
            int[] cat = ClosestCat();
            int[] cheese = ClosestCheese();
            if (cat[2] > cheese[2])
            {
                if (cheese[0] > YCoordinate && Map[YCoordinate + 1][XCoordinate] == null)
                {
                    YCoordinate++;
                }
                else if (cheese[0] < YCoordinate && Map[YCoordinate - 1][XCoordinate] == null)
                {
                    YCoordinate--;
                }
                else if (cheese[1] > XCoordinate && Map[YCoordinate][XCoordinate + 1] == null)
                {
                    XCoordinate++;
                }
                else if (Map[YCoordinate][XCoordinate - 1] == null)
                {
                    XCoordinate--;
                }
            }
            else
            {
                if (cat[0] > YCoordinate && Map[YCoordinate - 1][XCoordinate] == null)
                {
                    YCoordinate--;
                }
                else if (cat[0] < YCoordinate && Map[YCoordinate + 1][XCoordinate] == null)
                {
                    YCoordinate++;
                }
                else if (cat[1] > XCoordinate && Map[YCoordinate][XCoordinate - 1] == null)
                {
                    XCoordinate--;
                }
                else if (Map[YCoordinate][XCoordinate + 1] == null)
                {
                    XCoordinate++;
                }
            }
        }

        public void EndOfTurn()
        {
            FoodPoints--;
            Eat();
            Breed();
            Move();
        }

    }
}