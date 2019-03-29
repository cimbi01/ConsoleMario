using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Devices;

namespace ConsoleMario
{
    public static class Game
    {
        // default Console Background Color and Default Color to change on cursor
        private static readonly ConsoleColor default_bgcolor = ConsoleColor.Black;
        private static readonly ConsoleColor change_bgcolor = ConsoleColor.DarkRed;
        // Describes that player want to exit
        private static bool exited = false;
        // Describes all the existed Paths returned by path.init
        private static readonly List<Path> paths = Path.Init();
        // Describes the player
        private static readonly Player player = new Player();
        // Describes the max of levels of the player won -1
        private static int player_maxLevel = 0;
        // Describes the actual level
        private static int actual_level = 0;
        // Describes the actual path
        private static Path actual_path;
        public static void Play()
        {
            // cursor will not be visible
            Console.CursorVisible = false;
            // while player does not want to exit
            while (!exited)
            {
                player.Reset();
                actual_path = new Path(paths[actual_level]);
                Console.Clear();
                RenderActualPath();
                // while player doesnt win and player is alive
                while (!player.Win && player.Life > 0)
                {
                    Move();
                }
                // if player won
                if (player.Win)
                {
                    Console.Clear();
                    Console.WriteLine("You Win");
                    // if actual level = player_maxlevel
                    // then if playermaxlevel < path.maxlevel 
                    // then player_maxlevel++
                    if (actual_level == player_maxLevel && player_maxLevel < Path.MaxLevel)
                    {
                        player_maxLevel++;
                    }
                    // if actual level < Path.MaxLevel
                    // then actual level++ cause there is more level above
                    if (actual_level < Path.MaxLevel)
                    {
                        actual_level++;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You Lost");
                }
                System.Threading.Thread.Sleep(1000);
                exited = Path.MaxLevel == player_maxLevel;
            }
            // if player want to exit
            Console.WriteLine("Game over");
        }
        // Write the Actual Path but on x, y position write the player character
        private static void RenderActualPath()
        {
            for (int i = 0; i < actual_path.Devices.GetLength(0); i++)
            {
                for (int j = 0; j < actual_path.Devices.GetLength(1); j++)
                {
                    if (i == player.PositionX && j == player.PositionY)
                    {
                        Console.Write(player.Character);
                    }
                    else
                    {
                        Console.Write(actual_path.Devices[i, j].Character);
                    }
                }
                Console.WriteLine();
            }
        }
        private static void Move()
        {
            char ch = Console.ReadKey(true).KeyChar;
            player.Move(ch);
            RenderPlayer();
        }
        // render player and use the player positionx, positiony device of actual_path devices
        private static void RenderPlayer()
        {
            if (player.Life > 0)
            {
                // setcursor to previus position and write the previous device where it is
                Console.SetCursorPosition(player.PreviousPositionY, player.PreviousPositionX);
                Console.Write(actual_path.Devices[player.PreviousPositionX, player.PreviousPositionY].Character);
                Console.BackgroundColor = change_bgcolor;
                // setcursor to current position and write the current device where it is
                Console.SetCursorPosition(player.PositionY, player.PositionX);
                Console.Write(actual_path.Devices[player.PositionX, player.PositionY].Character);
                Console.BackgroundColor = default_bgcolor;
                Console.SetCursorPosition(player.PositionY, player.PositionX);
                System.Threading.Thread.Sleep(300);
                try
                {
                    actual_path.Devices[player.PositionX, player.PositionY].Use(player);
                }
                // if the door is closed 
                catch (DoorIsClosedException)
                {
                    // step back and renderplayer again
                    player.StepBack();
                    RenderPlayer();
                }
                // if run in wall
                catch (RunInWallException)
                {
                    // step back and renderplayer again
                    player.StepBack();
                    RenderPlayer();
                }
                // if on players previous position the device is spiral then renderplayer again
                if (actual_path.Devices[player.PreviousPositionX, player.PreviousPositionY] is Spiral)
                {
                    RenderPlayer();
                }
            }
        }
    }
}
