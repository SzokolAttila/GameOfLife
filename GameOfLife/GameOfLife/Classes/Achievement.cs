using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    internal class Achievement
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public bool IsDone { get; private set; }

        public bool CheckIfDone()
        {
            if (Achievements.Conditions(Name))
            {
                IsDone = true;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Name}\n\t{Description}\n{(IsDone?"Kész!":"Még vár rád!")}";
        }

        public Achievement(string row)
        {
            string[] splitted = row.Split(';');
            Name = splitted[0];
            Description = splitted[1];
            IsDone = splitted[2]!="0";
        }
    }
}
