using GameOfLife.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    internal class Cat : IAnimals
    {
        readonly List<Cat> Livecats = new List<Cat>();
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
                if (TurnsLived >= 5)
                {
                    speed = 2;
                }
                else
                {
                    speed = 1;
                }
            }
        }
        public int XCoordinate { get; private set; }

        public int YCoordinate { get; private set; }

        public char Display { get; init; }

        readonly private string name;
        public string Name
        { 
            get=>name;
            set
            {
                if (TurnsLived>=10)
                {
                    int r = random.Next(0,catNames.Count);
                    Name = catNames[r];
                }
                else
                {
                    Name = value;
                }
            }
        }
        public bool AdultKitten => TurnsLived >= 5;

        public void Breed()
        {
            foreach (var item in Livecats)
            {
                if ((item.XCoordinate==XCoordinate+1|| item.XCoordinate == XCoordinate - 1 && item.YCoordinate == YCoordinate + 1 || item.YCoordinate == YCoordinate - 1)
                    && TurnsLived>=5 && item.TurnsLived>=5)
                {
                    Cat littlecat = new Cat(XCoordinate,YCoordinate);
                    Livecats.Add(littlecat);
                }
            }
        }

        public void EndOfTurn()
        {
            TurnsLived += 1;
        }

        public void Move()
        {
            XCoordinate += speed;
            YCoordinate += speed;
        }

        public void Death()
        {
            Livecats.Remove(this);
        }

        public int Eat()
        {
            int mouseFoodPoint = 0;
            if (FoodPoints+mouseFoodPoint>MaxFoodPoints)
            {
                FoodPoints = MaxFoodPoints;
            }
            else
            {
                FoodPoints += mouseFoodPoint;
            }
            return FoodPoints;
            
        }
    }
}
