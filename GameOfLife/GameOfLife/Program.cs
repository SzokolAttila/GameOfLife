﻿using GameOfLife.Classes;
using GameOfLife.Interfaces;

var random = new Random();
Menu menu = new Menu();
int selectedIndex = 0;
string[] buttons = { "Új játék", "Magyarázat"};

while (true)
{
    Console.SetCursorPosition(0, 2);
    Console.WriteLine(menu.Title());
    Console.WriteLine();

    for (int i = 0; i < buttons.Length; i++)
    {
        if (i == selectedIndex)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        Console.WriteLine(buttons[i]);
        Console.ResetColor();
    }
    Console.WriteLine();
    Console.ForegroundColor= ConsoleColor.DarkGray;
    Console.WriteLine("Kilépni 'Esc' gombbal tudsz.");
    Console.ResetColor();
    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

    switch (keyInfo.Key)
    {
        case ConsoleKey.UpArrow:
            if (selectedIndex > 0)
                selectedIndex--;
            break;

        case ConsoleKey.DownArrow:
            if (selectedIndex < buttons.Length - 1)
                selectedIndex++;
            break;

        case ConsoleKey.Enter:
            Console.Clear();
            if (selectedIndex ==0)
            {
                Console.Clear();
                Console.WriteLine("Adja meg a következő adatokat:");
                while (true)
                {
                    try
                    {
                        Console.Write("Pálya magassága: ");
                        int maxHeight = int.Parse(Console.ReadLine()!);
                        Grid.MaxHeight = maxHeight;
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
                while (true)
                {
                    try
                    {
                        Console.Write("Pálya szélessége: ");
                        int maxWidth = int.Parse(Console.ReadLine()!);
                        Grid.MaxWidth = maxWidth;
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
                Grid.Map = new Tile[Grid.MaxHeight, Grid.MaxWidth];
                while (true)
                {
                    try
                    {
                        Console.Write("Macskák száma: ");
                        int catsNumber = int.Parse(Console.ReadLine()!);
                        Grid.NumberOfCats = catsNumber;
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
                while (true)
                {
                    try
                    {
                        Console.Write("Egerek száma: ");
                        int miceNumber = int.Parse(Console.ReadLine()!);
                        Grid.NumberOfMice = miceNumber;
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
                while (true)
                {
                    try
                    {
                        Console.Write("Konyhásnénik száma: ");
                        int scullionsNumber = int.Parse(Console.ReadLine()!);
                        Grid.NumberOfScullions = scullionsNumber;
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
                while (true)
                {
                    try
                    {
                        Console.Write("Sajtok száma: ");
                        int cheesesNumber = int.Parse(Console.ReadLine()!);
                        Grid.NumberOfCheeses = cheesesNumber;
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
                Console.Clear();

                InitiateMap();
                SpawnCheeses();
                SpawnCats();
                SpawnMice();
                SpawnScullions();
                DrawGrid();
                DrawEntities();

                while (Grid.NumberOfCats > 0 && Grid.NumberOfMice > 0)
                {
                    Console.CursorVisible = false;
                    Console.ReadKey(true);
                    for (int i = 0; i < Grid.MaxHeight; i++)
                    {
                        for (int j = 0; j < Grid.MaxWidth; j++)
                        {
                            var currentTile = Grid.Map[i, j].Content.OrderBy(x => x.GetType().ToString()).ToList();
                            for (var k = 0; k < currentTile.Count; k++)
                            {
                                currentTile[k].EndOfTurn();
                            }
                        }
                    }
                    for (int i = 0; i < Grid.MaxHeight; i++)
                    {
                        for (int j = 0; j < Grid.MaxWidth; j++)
                        {
                            var currentTile = Grid.Map[i, j].Content.OrderBy(x => x.GetType().ToString()).ToList();
                            foreach (var entity in currentTile)
                            {
                                var to = Grid.Map[entity.YCoordinate, entity.XCoordinate];
                                Grid.Map[i, j].Content.Remove(entity);
                                to.Content.Add(entity);
                            }
                            Grid.Map[i, j].RestoreVariables();
                        }
                    }
                    EndOfTurnSum();
                    DrawGrid();
                    DrawEntities();
                }
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (selectedIndex==1)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Magyarázat a játékhoz!\n");
                Console.ResetColor();
                Console.WriteLine(menu.Description());
                Console.ReadKey(true);
                Console.Clear();
            }
            break;

        case ConsoleKey.Escape:
            return;
    }
}


void InitiateMap()
{
    for (var i = 0; i < Grid.MaxHeight; ++i)
    {
        for (var j = 0; j < Grid.MaxWidth; ++j)
            Grid.Map[i, j] = new Tile(j, i);
    }
}

void SpawnCheeses()
{
    var currentNumber = 0;
    while (currentNumber < Grid.NumberOfCheeses)
    {
        var x = random.Next(Grid.MaxWidth);
        var y = random.Next(Grid.MaxHeight);
        if (Grid.Map[y, x].HasEntity("GameOfLife.Classes.Cheese")) 
            continue;
        Grid.Map[y, x].Content.Add(new Cheese(x, y));
        ++currentNumber;
    }
}

void SpawnMice()
{
    var currentNumber = 0;
    while (currentNumber < Grid.NumberOfMice)
    {
        var x = random.Next(Grid.MaxWidth);
        var y = random.Next(Grid.MaxHeight);
        if (Grid.Map[y, x].HasEntity("GameOfLife.Classes.Mouse") || Grid.Map[y, x].HasEntity("GameOfLife.Classes.Cat")) 
            continue;
        Grid.Map[y, x].Content.Add(new Mouse(x, y));
        ++currentNumber;
    }
}

void SpawnCats()
{
    var currentNumber = 0;
    while (currentNumber < Grid.NumberOfCats)
    {
        var x = random.Next(Grid.MaxWidth);
        var y = random.Next(Grid.MaxHeight);
        if (Grid.Map[y, x].HasEntity("GameOfLife.Classes.Cat")) 
            continue;
        Grid.Map[y, x].Content.Add(new Cat(x, y));
        ++currentNumber;
    }
}

void SpawnScullions()
{
    var currentNumber = 0;
    while (currentNumber < Grid.NumberOfScullions)
    {
        var x = random.Next(Grid.MaxWidth);
        var y = random.Next(Grid.MaxHeight);
        if (Grid.Map[y, x].HasEntity("GameOfLife.Classes.Scullion")) 
            continue;
        Grid.Map[y, x].Content.Add(new Scullion(x, y));
        ++currentNumber;
    }
}

void DrawGrid()
{
    for (var i = 0; i < Grid.MaxHeight + 1; ++i)
    {
        for (var j = 0; j < Grid.MaxWidth * 4 + 1; ++j)
        {
            Console.SetCursorPosition(j, i * 3);
            Console.Write("_"); 
        }
    }
    for (var i = 0; i < Grid.MaxWidth + 1; ++i)
    {
        for (var j = 1; j < Grid.MaxHeight * 3 + 1; ++j)
        {
            Console.SetCursorPosition(i * 4, j);
            Console.Write("|");
        }
    }
}

void DrawEntities()
{
    for (var i = 0; i < Grid.MaxHeight; ++i)
    {
        for (var j = 0; j < Grid.MaxWidth; ++j)
        {
            var drawnEntities = 0;
            var vOffSet = 0;
            var hOffSet = 0;
            foreach (var entity in Grid.Map[i, j].Content)
            {
                if (drawnEntities == 2)
                {
                    ++vOffSet;
                    hOffSet = 0;
                }
                Console.SetCursorPosition(1 + j * 4 + hOffSet, 1 + i * 3 + vOffSet);
                var display = entity.Display;
                switch (entity.GetType().ToString())
                {
                    case "GameOfLife.Classes.Mouse":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case "GameOfLife.Classes.Cheese":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case "GameOfLife.Classes.Cat":
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case "GameOfLife.Classes.Scullion":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                }
                Console.Write(display);
                Console.ForegroundColor = ConsoleColor.White;
                hOffSet += 2;
                ++drawnEntities;
            }
        }
    }
}

void ClearEntities()
{
    for (var i = 0; i < Grid.MaxHeight; ++i)
    {
        for (var j = 0; j < Grid.MaxWidth; ++j)
        {
            var vOffSet = 0;
            var hOffSet = 0;
            for (var k = 0; k < 4; k++)
            {
                if (k == 2)
                {
                    ++vOffSet;
                    hOffSet = 0;
                }
                Console.SetCursorPosition(1 + j * 4 + hOffSet, 1 + i * 3 + vOffSet);
                Console.Write(" ");
                hOffSet += 2;
            }
        }
    }
}

void EndOfTurnSum()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.Write("Macskák: ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"Elpusztult {Cat.Dead} db és született {Cat.Born} db --> Összesen {Grid.NumberOfCats} db él");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write("Egerek: ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"Elpusztult {Mouse.Dead} db és született {Mouse.Born} db --> Összesen {Grid.NumberOfMice} db él");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Sajtok: ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"Megettek {Cheese.Eaten} darabot és lekerült {Cheese.Placed} db --> Összesen {Grid.NumberOfCheeses} db van a pályán");
    Console.WriteLine("Nyomj entert a továbblépéshez!");
    Console.ReadLine();

    if (Cat.DeadCats.Count>0)
    {
        Console.WriteLine("In memoriam");
        Console.WriteLine(string.Join("\t", Cat.DeadCats.Select(x=>$"{x.Name}, élt {x.TurnsLived} körig")));
        Console.WriteLine("1 perc néma csend után nyomj entert, hogy továbblépj!");
        Console.ReadLine();
    }
    Console.Clear();

    Cat.Born = 0;
    Cat.Dead = 0;
    Mouse.Born = 0;
    Mouse.Dead = 0;
    Cheese.Placed = 0;
    Cheese.Eaten = 0;
}