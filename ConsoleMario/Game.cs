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
        // Describes that player want to exit
        private static bool exited = false;
        // Describes all the existed Paths returned by path.init
        private static List<Path> paths = Path.Init();
        // Describes the player
        private static readonly Player player = new Player();
        // Describes the max of levels of the player won -1
        private static int player_maxLevel = 1;
        // Describes the actual level
        private static int actual_level = 1;
        // Describes the actual path
        private static Path actual_path = new Path(paths[actual_level]);
        public static void Play()
        {
            // while player does not want to exit
            while (!exited)
            {
                Console.Clear();
                RenderActualPath();
                // while player doesnt win and player is alive
                while (!player.Win && player.Live)
                {
                    Move();
                }
                // if player won
                if (player.Win)
                {
                    Console.Clear();
                    Console.WriteLine("You Win");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You Lost");
                }
            }
            // if player want to exit
        }
        // Write all the device's character in row and column to console
        private static void RenderActualPath()
        {
            for (int i = 0; i < actual_path.Devices.GetLength(0); i++)
            {
                for (int j = 0; j < actual_path.Devices.GetLength(1); j++)
                {
                    Console.Write(actual_path.Devices[i, j].Character);
                }
                Console.Write('\n');
            }
        }
        // Write the Actual Path but on x, y position write the player character
        private static void RenderActulPath(int x, int y)
        {
            for (int i = 0; i < actual_path.Devices.GetLength(0); i++)
            {
                for (int j = 0; j < actual_path.Devices.GetLength(1); j++)
                {
                    if (i == x && j == y)
                    {
                        Console.Write(player.Character);
                    }
                    else
                    {
                        Console.Write(actual_path.Devices[i, j].Character);
                    }
                }
                Console.Write('\n');
            }
        }
        private static void Move()
        {
            char ch = Console.ReadKey().KeyChar;
            player.Move(ch);
            RenderPlayer();
        }
        // render player and use the player positionx, positiony device of actual_path devices
        private static void RenderPlayer()
        {
            Console.Clear();
            RenderActulPath(player.PositionX, player.PositionY);
            System.Threading.Thread.Sleep(2000);
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
            // if on players position the device is spiral then renderplayer again
            if (actual_path.Devices[player.PositionX, player.PositionY] is Spiral)
            {
                RenderPlayer();
            }
        }
    }
}
