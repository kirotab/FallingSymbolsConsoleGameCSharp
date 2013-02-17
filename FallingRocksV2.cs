// Exercise 11 Falling Rocks
// In order to experience the game at it's fullest
// you will need to set your console font to utf friendly :D
// enjoy ;)

// RULES
//VERSION 2.0 
//1. Added Player Fire - Spacebar
//   player gain more points from shooting;
//   but don't gain health from shot hearts :(
//
//2. Way more playable (refresh and redraw at 2 iterations
//   but player moves at every iteration)



using System;
using System.Text;
using System.Threading;


class FallingRocksV2
{
    //Global Variables
    static int speed = 105;
    static int level = 1;
    //Player
    static int playerX = 0;
    static int playerY = 0;
    static int playerHP = 5;
    static decimal playerScore = 0;
    static decimal bestScore = 0;
    static int bulletX = -1;
    static int bulletY = -1;
    static int bulletType = 1;
    //Rocks
    static int[] rocksX = new int[160];
    static int[] rocksY = new int[160];
    static char[] rocksType = new char[160];
    static char[] typesOfRocks = { '^', '@', '*', '&', '+', '%', '$', '#', '!', '♦', '~', '♠', '♥' };
    static int[] rocksSize = new int[160];
    static int[] rocksColor = new int[160];
    static Random randomGenerator = new Random();
    //Counter
    static int counterOfTurns = 0;

    static void RemoveScrollBars()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.OutputEncoding = Encoding.UTF8;
    }

    static void ResetGame()
    {
        SetInitialPlayerPosition();
        playerHP = 5;
        counterOfTurns = 0;
        playerScore = 0;
        level = 1;
    }

    static void SetInitialPlayerPosition()
    {
        for (int resetRock = 0; resetRock < 160; resetRock++)
        {
            rocksX[resetRock] = -1;
            rocksY[resetRock] = -1;
        }
        playerY = Console.WindowHeight - 2;
        playerX = Console.WindowWidth / 2 - 15 - 2;
    }

    static void setColor(int color)
    {
        switch (color)
        {
            case 0: Console.ForegroundColor = ConsoleColor.White;
                break;
            case 1: Console.ForegroundColor = ConsoleColor.DarkCyan;
                break;
            case 2: Console.ForegroundColor = ConsoleColor.Red;
                break;
            case 3: Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case 4: Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case 5: Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case 6: Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case 7: Console.ForegroundColor = ConsoleColor.Gray;
                break;
            case 8: Console.ForegroundColor = ConsoleColor.Green;
                break;
            case 9: Console.ForegroundColor = ConsoleColor.DarkGreen;
                break;
            default:
                break;
        }
    }

    static void PrintAtPosition(int x, int y, char symbol, int color)
    {
        setColor(color);
        Console.SetCursorPosition(x, y);
        Console.Write(symbol);
    }
    static void DrawBullet()
    {
        if (bulletY > 0)
        {
            if (bulletType == 1)
            {
                PrintAtPosition(bulletX, bulletY, '♦', 0);
            }
        }
    }
    static void DrawPlayer()
    {
        PrintAtPosition(playerX, playerY, '(', 0);
        PrintAtPosition(playerX + 1, playerY, '0', 6);
        PrintAtPosition(playerX + 2, playerY, ')', 0);

    }

    static void DrawScoreboard()
    {
        for (int y = 0; y < Console.WindowHeight; y += 3)
        {
            PrintAtPosition(Console.WindowWidth - 30, y, '|', 0);
        }
        Console.SetCursorPosition(Console.WindowWidth - 28, 1);
        Console.WriteLine("Hit Points | ");
        for (int hp = 0; hp < playerHP; hp++)
        {
            PrintAtPosition(Console.WindowWidth - 13 + hp + hp, 1, '♥', 2);//♥♦♠
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(Console.WindowWidth - 28, 3);
        Console.WriteLine("Your Score |  {0,10}", playerScore);
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.SetCursorPosition(Console.WindowWidth - 28, 5);
        Console.WriteLine("Best Score |  {0,10}", bestScore);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(Console.WindowWidth - 28, 7);
        Console.WriteLine(" L E V E L |  {0,10}", level);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.SetCursorPosition(Console.WindowWidth - 28, 9);
        Console.WriteLine("Speed mSec |  {0,10}", level <= 10 ? (speed - (level * 5)) : (speed));
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.SetCursorPosition(Console.WindowWidth - 28, Console.WindowHeight - 2);
        Console.Write("© Falling RocksV2 by ENjOYy");
        for (int hp = 0; hp < playerHP; hp++)
        {
            PrintAtPosition(1 + hp + hp, Console.WindowHeight - 1, '♥', 2);//♥♦♠
        }

    }

    static void MovePlayerRight()
    {
        if (playerX < Console.WindowWidth - 33)
        {
            playerX++;
        }
    }
    static void MovePlayerLeft()
    {
        if (playerX > 0)
        {
            playerX--;
        }
    }
    static void fire()
    {
        bulletX = playerX + 1;
        bulletY = playerY;
    }
    static void MoveBullet()
    {
        if (bulletY > 1)
        {
            bulletY --;
            for(int numbRock = 0; numbRock < 144; numbRock++) 
            {
                if (bulletY < rocksY[numbRock]+1 || bulletY < rocksY[numbRock])
                {
                    if (bulletX >= rocksX[numbRock] && bulletX <= rocksX[numbRock] + rocksSize[numbRock] - 1)
                    {
                        playerScore += (rocksSize[numbRock] * 2);
                        rocksX[numbRock] = -1;
                        rocksY[numbRock] = -1;
                        rocksSize[numbRock] = 0;
                        bulletX = -1;
                        bulletY = -1;
                    }
                }
            }
        }
        else
        {
            bulletY = -1;
            bulletX = -1;
        }
    }

    static void playerIsHit(int rockType, int amountOfImpact)
    {
        if (rockType == typesOfRocks[12])
        {
            playerHP += amountOfImpact;
            if (playerHP > 5)
                playerHP = 5;
        }
        else
        {
            playerHP -= amountOfImpact;
        }

        if (playerHP < 1)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight / 2 - 3);
            Console.WriteLine("Game Over!");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 27, Console.WindowHeight / 2 + 2 - 3);
            Console.WriteLine("Press space to continue");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            while (keyInfo.Key != ConsoleKey.Spacebar)
            {
                keyInfo = Console.ReadKey();
            }
            playerHP = 3;
            ResetGame();
        }
    }
    static void DrawRocks()
    {
        for (int rockNumber = 0; rockNumber < 160; rockNumber++)
        {
            if (rocksX[rockNumber] >= 0 && rocksY[rockNumber] >= 0)
            {
                for (int drawRockCounter = 0; drawRockCounter < rocksSize[rockNumber]; drawRockCounter++)
                {
                    PrintAtPosition(rocksX[rockNumber] + drawRockCounter, rocksY[rockNumber], rocksType[rockNumber], rocksColor[rockNumber]);
                }
            }
        }
    }

    static void MoveRocks()
    {
        for (int numberRock = 0; numberRock < 160; numberRock++)
        {
            if (rocksX[numberRock] >= 0 && rocksY[numberRock] >= 0)
            {
                if (rocksY[numberRock] < Console.WindowHeight - 2)
                {
                    if (counterOfTurns % 2 != 0)
                    {
                        rocksY[numberRock]++;
                    }
                }
                else
                {
                    playerScore += rocksSize[numberRock];
                    if (bestScore < playerScore)
                        bestScore = playerScore;
                    if ((rocksX[numberRock] + rocksSize[numberRock] - 1) >= playerX && rocksX[numberRock] <= playerX + 2)
                    {
                        playerIsHit(rocksType[numberRock], rocksSize[numberRock]);
                    }
                    rocksX[numberRock] = -1;
                    rocksY[numberRock] = -1;
                }
            }
        }
    }

    static void GenerateRocks()
    {

        int numberElements = 5;
        for (int i = 0; i < numberElements; i++)
        {
            int randomNumber = randomGenerator.Next(0, 13);
            rocksType[counterOfTurns + i] = typesOfRocks[randomNumber];
            if (randomNumber != 12)
            {
                randomNumber = randomGenerator.Next(0, 10);
                rocksColor[counterOfTurns + i] = randomNumber;
            }
            else
            {
                rocksColor[counterOfTurns + i] = 2;
            }
            randomNumber = randomGenerator.Next(1, 4);
            rocksSize[counterOfTurns + i] = randomNumber;
            randomNumber = randomGenerator.Next((i * 10), (i * 10) + 11 - rocksSize[counterOfTurns + i]);
            rocksX[counterOfTurns + i] = randomNumber;
            rocksY[counterOfTurns + i] = 0;


        }

    }

    static void TurnCounter()
    {
        if (playerScore > 200 * level*level)
        {
            level++;
        }
        if (counterOfTurns > 143)
        {
            counterOfTurns = 0;
        }
        else
        {
            counterOfTurns++;
        }
    }

    static void Main()
    {
        RemoveScrollBars();
        SetInitialPlayerPosition();
        while (true)
        {
            if (counterOfTurns % 11 >= 10)
            {
                GenerateRocks();
            }
            
            if (counterOfTurns % 2 != 0)
            {
                
                MoveRocks();  
            }
            MoveBullet();
            
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    MovePlayerLeft();
                }
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    MovePlayerRight();
                }

                //if (keyInfo.Modifiers == ConsoleModifiers.Control)
                if (keyInfo.Key == ConsoleKey.Spacebar && bulletY < 0)
                {
                    fire();
                }
            }

            if (counterOfTurns % 2 != 0)
            {
                Console.Clear();
                DrawBullet();
                DrawPlayer();
                DrawScoreboard();
                DrawRocks();
            }
            TurnCounter();
            if (level <= 10)
            {
                Thread.Sleep(speed - (level * 5));
            }
            else
            {
                Thread.Sleep(speed - 55);
            }
        }
    }
}