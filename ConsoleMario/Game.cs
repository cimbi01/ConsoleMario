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
        private static bool messages_visible = false;
        // List of messages by Pathlevel
        private static List<string> messages = new List<string>();
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
        // Describes the render path
        private static Path renderpath;
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
                // if the player win the example path of the actual_path
                // or if the actual path has no example path
                // then render the actual_path
                if (player.ExamplePathWin || actual_path.ExamplePath == null)
                {
                    renderpath = actual_path;
                }
                // if isnt won &&& has example path
                // then render the example path of it
                else
                {
                    renderpath = actual_path.ExamplePath;
                }
                RenderRenderPath();
                // while player doesnt win and player is alive
                while (!player.Win && player.Life > 0)
                {
                    Move();
                }
                // if player won
                if (player.Win)
                {
                    Console.Clear();
                    string wintext = "You Win!";
                    if (renderpath is ExamplePath)
                    {
                        wintext += "\nWelldone!\nYou can move to the actual Path from the example!";
                    }
                    Console.WriteLine(wintext);
                    // if actual level = player_maxlevel
                    // && if the rendered path is not Example path
                    // && if playermaxlevel < path.maxlevel 
                    // then player_maxlevel++
                    if (actual_level == player_maxLevel && player_maxLevel < Path.MaxLevel && !(renderpath is ExamplePath))
                    {
                        player_maxLevel++;
                        messages.Add("");
                    }
                    // if actual level < Path.MaxLevel && the renderpath is not examplepath
                    // then actual level++ cause there is more level above
                    if (actual_level < Path.MaxLevel && !(renderpath is ExamplePath))
                    {
                        actual_level++;
                        player.ExamplePathWin = false;
                    }
                    else if (renderpath is ExamplePath)
                    {
                        player.ExamplePathWin = true;
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
        static Game()
        {
            for (int i = 0; i < player_maxLevel+1; i++)
            {
                messages.Add("");
            }
        }
        // Write the Actual Path but on x, y position write the player character
        private static void RenderRenderPath()
        {
            for (int i = 0; i < renderpath.Devices.GetLength(0); i++)
            {
                for (int j = 0; j < renderpath.Devices.GetLength(1); j++)
                {
                    if (i == player.PositionX && j == player.PositionY)
                    {
                        Console.Write(player.Character);
                    }
                    else
                    {
                        Console.Write(renderpath.Devices[i, j].Character);
                    }
                }
                Console.WriteLine();
            }
            // if the path is Example Path
            // then Write The Preview
            if (renderpath is ExamplePath)
            {
                Console.WriteLine();
                Console.WriteLine((renderpath as ExamplePath).Preview);
                Console.WriteLine();
            }
            // write all the messages by the Path
            AddMessage(messages[actual_level], false);
        }
        private static void Move()
        {
            char ch = Console.ReadKey(true).KeyChar;
            player.Move(ch);
            AddMessage("Playered moved x Direction: " + Convert.ToString(player.PositionX-player.PreviousPositionX) +
                " y Direction: " + Convert.ToString(player.PositionY - player.PreviousPositionY));
            RenderPlayer();
        }
        // render player and use the player positionx, positiony device of actual_path devices
        private static void RenderPlayer()
        {
            if (player.Life > 0)
            {
                // setcursor to previus position and write the previous device where it is
                Console.SetCursorPosition(player.PreviousPositionY, player.PreviousPositionX);
                Console.Write(renderpath.Devices[player.PreviousPositionX, player.PreviousPositionY].Character);
                // setcursor to current position and write the current device where it is
                Console.BackgroundColor = change_bgcolor;
                Console.SetCursorPosition(player.PositionY, player.PositionX);
                Console.Write(renderpath.Devices[player.PositionX, player.PositionY].Character);
                Console.BackgroundColor = default_bgcolor;
                Console.SetCursorPosition(player.PositionY, player.PositionX);
                System.Threading.Thread.Sleep(300);
                try
                {
                    AddMessage("Device: " + Convert.ToString(renderpath.Devices[player.PositionX, player.PositionY].GetType()));
                    renderpath.Devices[player.PositionX, player.PositionY].Use(player);
                }
                // if the door is closed 
                catch (DoorIsClosedException)
                {
                    AddMessage("Run in Closed Door");
                    player.RenderNeeded = false;
                    // step back and renderplayer again
                    player.StepBack();
                    AddMessage("Playered moved x Direction: " + Convert.ToString(player.PositionX - player.PreviousPositionX) +
                        " y Direction: " + Convert.ToString(player.PositionY - player.PreviousPositionY));
                    RenderPlayer();
                }
                // if run in wall
                catch (RunInWallException)
                {
                    AddMessage("Run in Wall");
                    player.RenderNeeded = false;
                    // step back and renderplayer again
                    player.StepBack();
                    AddMessage("Playered moved x Direction: " + Convert.ToString(player.PositionX - player.PreviousPositionX) +
                        " y Direction: " + Convert.ToString(player.PositionY - player.PreviousPositionY));
                    RenderPlayer();
                }
                // if player need rerender
                if (player.RenderNeeded)
                {
                    AddMessage("Playered moved x Direction: " + Convert.ToString(player.PositionX - player.PreviousPositionX) +
                        " y Direction: " + Convert.ToString(player.PositionY - player.PreviousPositionY));
                    player.RenderNeeded = false;
                    RenderPlayer();
                }
            }
        }
        // Add message to messages and write it under Path
        private static void AddMessage(string message, bool add = true)
        {
            if (add)
            {
                messages[actual_level] += '\n' + message;
            }
            int x = renderpath.Column, y = renderpath.Row + 1;
            // if renderpath is ExamplePath then add to y the number of rows of the preview
            if (renderpath is ExamplePath)
            {
                y += (renderpath as ExamplePath).Preview.Split('\n').Length;
            }
            if (messages_visible)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(messages[actual_level]);
                Console.SetCursorPosition(player.PositionY, player.PositionX);
            }
        }
    }
}
