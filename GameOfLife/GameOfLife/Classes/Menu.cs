using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GameOfLife.Classes
{
    public class Menu
    {
        public string Title()
        {
            return $" _______  _______ __________________ _______  _______    _______  _______  _______  _______  _        _______ \n" +
                $"(  ____ \\(  ___  )\\__    _/\\__   __/(  ___  )(  ____ \\  (       )(  ___  )(  ____ \\(  ____ \\| \\    /\\(  ___  )\n" +
                $"| (    \\/| (   ) |   )  (     ) (   | (   ) || (    \\/  | () () || (   ) || (    \\/| (    \\/|  \\  / /| (   ) |\n" +
                $"| (_____ | (___) |   |  |     | |   | |   | || (_____   | || || || (___) || |      | (_____ |  (_/ / | (___) |\n" +
                $"(_____  )|  ___  |   |  |     | |   | |   | |(_____  )  | |(_)| ||  ___  || |      (_____  )|   _ (  |  ___  |\n" +
                $"      ) || (   ) |   |  |     | |   | |   | |      ) |  | |   | || (   ) || |            ) ||  ( \\ \\ | (   ) |\n" +
                $"/\\____) || )   ( ||\\_)  )     | |   | (___) |/\\____) |  | )   ( || )   ( || (____/\\/\\____) ||  /  \\ \\| )   ( |\n" +
                $"\\_______)|/     \\|(____/      )_(   (_______)\\_______)  |/     \\||/     \\|(_______/\\_______)|_/    \\/|/     \\|\n";
        }

        public string Description()
        {
            string charachters = "Karakterek: " +
                "\tSajtdarab: s\n" +
                "\tFriss sajt: S\n" +
                "\tÉrett sajt: E\n" +
                "\tPenészes sajt: P\n" +
                "\tEgér: e\n" +
                "\tKiscica: c\n" +
                "\tNagycica: C\n" +
                "\tKonyhás néni: K\n";
            return "Asd";
        }
    }
}
