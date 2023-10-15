using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    internal static class Achievements
    {

        public static List<Achievement> Collection = new List<Achievement>();

        public static bool Conditions(string name)
        {
            return name switch
            {
                "Macskás néni" => (Grid.NumberOfMice <= 0),
                "Egerek ura" => (Grid.NumberOfCats <= 0),
                "Sajtimádó" => (Grid.NumberOfCheeses == Grid.MaxHeight * Grid.MaxWidth),
                "Egértanya" => (Grid.NumberOfMice >= (Grid.MaxHeight * Grid.MaxWidth) / 2),
                "Macskafarm" => (Grid.NumberOfCats >= (Grid.MaxHeight * Grid.MaxWidth) / 2),
                "Etető kéz" => (Scullion.CatsFed == Grid.NumberOfScullions && Grid.NumberOfScullions > 0),
                "Agresszor" => (Scullion.MiceStunned == Grid.NumberOfScullions && Grid.NumberOfScullions > 0),
                _ => false
            };
        }
        

        public static string CheckAll()
        {
            List<string> done = new List<string>();
            foreach (Achievement achievement in Collection.Where(x => !x.IsDone))
            {
                if (achievement.CheckIfDone())
                {
                    done.Add(achievement.Name);
                }
            }
            if (done.Count > 0)
            {
                return $"Új küldetéseket teljesítettél!\n{string.Join("\n\t>>>", done)}";
            }
            return "";
        }

        public static void Initiate(string filename)
        {
            foreach (string row in File.ReadAllLines(filename))
            {
                Collection.Add(new Achievement(row));
            }
        }

        public static void Save(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            foreach (Achievement a in Collection)
            {
                sw.WriteLine($"{a.Name};{a.Description};{(a.IsDone?"1":"0")}");
            }
            sw.Close();
        }

        public static string ListCollection()
        {
            return string.Join("\n\n", Collection.Select(x=>x.ToString()));
        }

    }
}

