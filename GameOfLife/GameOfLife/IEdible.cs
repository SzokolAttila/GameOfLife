﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal interface IEdible : IEntity
    {
        int FoodPoints { get; set; }
        void Death();
    }
}
