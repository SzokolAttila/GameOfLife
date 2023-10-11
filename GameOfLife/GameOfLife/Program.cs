﻿using GameOfLife.Classes;

var random = new Random();
Grid.MaxHeight = 10;
Grid.MaxWidth = 10;
Grid.Map = new Tile[Grid.MaxHeight, Grid.MaxWidth];
Grid.NumberOfCats = 2;
Grid.NumberOfMice = 50;
Grid.NumberOfScullions = 1;
Grid.NumberOfCheeses = 80;

InitiateMap();
SpawnCheeses();
SpawnMice();
SpawnCats();
SpawnScullions();
DrawGrid();
DrawEntities();
ClearEntities();

while(Grid.NumberOfCats > 1 && Grid.NumberOfMice > 1)
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
            for (var k = 0; k < Grid.Map[i, j].Content.Count; k++)
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