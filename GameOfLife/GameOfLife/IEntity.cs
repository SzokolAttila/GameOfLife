﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal interface IEntity
    {
        int[] Position { get; }
        void EndOfTurn();
        char Display { get; }
    }
}
