using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
            string charachters = "Karakterek:\n" +
                "\tSajtdarab: s\n" +
                "\tFriss sajt: S\n" +
                "\tÉrett sajt: E\n" +
                "\tPenészes sajt: P\n" +
                "\tEgér: e\n" +
                "\tKiscica: c\n" +
                "\tNagycica: C\n" +
                "\tKonyhás néni: K";
            string rounds = "Játékmenet: \n" +
                "A következő körhöz nyomj le egy billentyűt. Körönként mozognak a karakterek, esznek, szaporodnak és meghalnak.\n";
            string emptyRow = "\n\n";
            string rules = "Szabályok:\n" +
                "\t-A játékban négy entitás lesz, sajt, egér, macska és konyhásnéni\n" +
                "\t-A konyhásnénin kívül mindenkinek van kajapontja, ami körönként 1-el csökken\n" +
                "\t-A játéknak akkor van vége, ha vagy az összes egér, vagy az összes macska elpusztult.\n" +
                "\t-Az entitások akkor esznek, hogyha olyan mezőn állnak, amit meg tudnak enni.\n" +
                "\t-A sajtnak négy fázisa van: sajtdarab, friss sajt, érett sajt és penészes sajt\n" +
                "\t-A friss sajt, az érett sajt és a penészes sajt elfogyasztható (1, 2 és 3 pontot adnak)\n" +
                "\t-Az egér a sajtot eszi, hogyha van a mellette lévő (nem átlós) mezőn másik egér, szaporodik. (Egér kajapont / 2) kajapontot ér \n" +
                "\t-Az egér alapból 3 kajaponttal kezd, és maximum 7 kajapontja lehet.\n" +
                "\t-A macska egeret eszik, ha van mellette lévő (nem átlós) mezőn másik macska, szaporodik\n" +
                "\t-A macska 5 kajaponttal kezd, és maximum 10 kajapontja lehet\n" +
                "\t-A konyhásnéni a macskát megeteti (1 ponttal), valamint az egeret megüti, " +
                "így az egér egy körig nem mozoghat. Ezen kívül friss sajtot szór a sajtdarabok helyére\n" +
                "\t-Az egér és a konyhás 1 egységet mozoghat egy körben, a macska 1-2 egységet\n" +
                "\t-A friss sajt körönként változik (érett sajt majd penészes sajt lesz)";
            return charachters + emptyRow + rules + emptyRow + rounds;
        }

        public string TheEnd()
        {
            return "#######                                       \n" +
                "   #    #    # ######    ###### #    # #####  \n" +
                "   #    #    # #         #      ##   # #    # \n" +
                "   #    ###### #####     #####  # #  # #    # \n" +
                "   #    #    # #         #      #  # # #    # \n" +
                "   #    #    # #         #      #   ## #    # \n" +
                "   #    #    # ######    ###### #    # ##### ";
        }
    }
}
