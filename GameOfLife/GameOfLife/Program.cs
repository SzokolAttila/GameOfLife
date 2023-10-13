using GameOfLife.Classes;

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
                Console.Write("Pálya magassága: ");
                int maxHeight = int.Parse(Console.ReadLine()!);
                Grid.MaxHeight = maxHeight;
                Console.Write("Pálya szélessége: ");
                int maxWidth = int.Parse(Console.ReadLine()!);
                Grid.MaxWidth = maxWidth;
                Grid.MaxHeight = maxHeight;
                Grid.Map = new Tile[Grid.MaxHeight, Grid.MaxWidth];
                Console.Write("Macskák száma: ");
                int catsNumber = int.Parse(Console.ReadLine()!);
                Grid.NumberOfCats = catsNumber;
                Console.Write("Egerek száma: ");
                int miceNumber = int.Parse(Console.ReadLine()!);
                Grid.NumberOfMice = miceNumber;
                Console.Write("Konyhásnénik száma: ");
                int scullionsNumber = int.Parse(Console.ReadLine()!);
                Grid.NumberOfScullions = scullionsNumber;
                Console.Write("Sajtok száma: ");
                int cheesesNumber = int.Parse(Console.ReadLine()!);
                Grid.NumberOfCheeses = cheesesNumber;
                Console.Clear();

                InitiateMap();
                SpawnCheeses();
                SpawnMice();
                SpawnCats();
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
                            for (var k = 0; k < Grid.Map[i, j].Content.Count; k++)
                            {
                                switch (Grid.Map[i, j].Content[k].GetType().ToString())
                                {
                                    case "GameOfLife.Classes.Mouse":
                                        var mouse = (Mouse)Grid.Map[i, j].Content[k];
                                        mouse.EndOfTurn();
                                        break;
                                    case "GameOfLife.Classes.Scullion":
                                        var scullion = (Scullion)Grid.Map[i, j].Content[k];
                                        scullion.EndOfTurn();
                                        break;
                                    case "GameOfLife.Classes.Cat":
                                        var cat = (Cat)Grid.Map[i, j].Content[k];
                                        cat.EndOfTurn();
                                        break;
                                    case "GameOfLife.Classes.Cheese":
                                        var cheese = (Cheese)Grid.Map[i, j].Content[k];
                                        cheese.EndOfTurn();
                                        break;
                                }
                            }
                        }
                    }
                    ClearEntities();
                    DrawEntities();
                }

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
        if (Grid.Map[y, x].HasEntity("GameOfLife.Classes.Mouse")) 
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
        for (var j = 0; j < Grid.MaxWidth * 4; ++j)
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
                var display = ' ';
                if (entity.GetType().ToString() == "GameOfLife.Classes.Mouse")
                {
                    var mouse = (Mouse)entity;
                    display = mouse.Display;
                }
                else if (entity.GetType().ToString() == "GameOfLife.Classes.Cheese")
                {
                    var cheese = (Cheese)entity;
                    display = cheese.Display;
                }
                else if (entity.GetType().ToString() == "GameOfLife.Classes.Cat")
                {
                    var cat = (Cat)entity;
                    display = cat.Display;
                }
                else if (entity.GetType().ToString() == "GameOfLife.Classes.Scullion")
                {
                    var scullion = (Scullion)entity;
                    display = scullion.Display;
                }
                Console.Write(display);
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