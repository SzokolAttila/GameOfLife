using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal interface IAlive : IEntity
    {
        int TurnsLived { get; set; }
        int Speed { get; set; }
        void Move();
    }
}
