using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal interface IAnimals : IEdible, IAlive
    {
        int Eat();
        int MaxFoodPoints { get; set; }
        void Breed();
    }
}
