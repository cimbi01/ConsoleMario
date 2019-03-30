using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario
{
    class Render
    {
        private static bool Messages_Visible { get; set; } = true;
        // default Console Background Color and Default Color to change on cursor
        private static readonly ConsoleColor default_bgcolor = ConsoleColor.Black;
        private static readonly ConsoleColor change_bgcolor = ConsoleColor.DarkRed;
        // Describes the render path
        public static Path Renderpath { set; get; }
        static Render()
        {
            Console.CursorVisible = false;
            for (int i = 0; i < Game.player_maxLevel+1; i++)
            {
                Game.Messages.Add(Convert.ToString((i + 1)) + ".level");
            }
        }
        // Write the Actual Path but on x, y position write the player character
        public static void RenderRenderPath()
        {
            Console.Clear();
            for (int i = 0; i < Renderpath.Devices.GetLength(0); i++)
            {
                for (int j = 0; j < Renderpath.Devices.GetLength(1); j++)
                {
                    if (i == Game.Player.PositionX && j == Game.Player.PositionY)
                    {
                        Console.Write(Game.Player.Character);
                    }
                    else
                    {
                        Console.Write(Renderpath.Devices[i, j].Character);
                    }
                }
                Console.WriteLine();
            }
            // if the path is Example Path
            // then Write The Preview
            if (Renderpath is ExamplePath)
            {
                Console.WriteLine();
                Console.WriteLine((Renderpath as ExamplePath).Preview);
                Console.WriteLine();
            }
            // write all the messages by the Path
            RenderMessages();
        }
        // render player and use the player positionx, positiony device of actual_path devices
        public static void RenderPlayer()
        {
            // setcursor to previus position and write the previous device where it is
            Console.SetCursorPosition(Game.Player.PreviousPositionY, Game.Player.PreviousPositionX);
            Console.Write(Renderpath.Devices[Game.Player.PreviousPositionX, Game.Player.PreviousPositionY].Character);
            // setcursor to current position and write the current device where it is
            Console.BackgroundColor = change_bgcolor;
            Console.SetCursorPosition(Game.Player.PositionY, Game.Player.PositionX);
            Console.Write(Renderpath.Devices[Game.Player.PositionX, Game.Player.PositionY].Character);
            Console.BackgroundColor = default_bgcolor;
            Console.SetCursorPosition(Game.Player.PositionY, Game.Player.PositionX);
            RenderMessages();
        }
        public static void RenderMessages()
        {
            int x = 0,
                y = Renderpath.Column + 1;
            if(Messages_Visible)
            {
                if (Renderpath is ExamplePath)
                {
                    y += (Renderpath as ExamplePath).Preview.Split('\n').Length + 2;
                }
                Console.SetCursorPosition(x, y);
                Console.Write(Game.Messages[Renderpath.LevelNumber-1]);
                Console.SetCursorPosition(Game.Player.PositionY, Game.Player.PositionX);
            }
        }
        public static void WriteWinMessage(bool win = true)
        {
            Console.Clear();
            string text = "";
            if (win)
            {
                text = "You Win!";
                if (Renderpath is ExamplePath)
                {
                    text += "\nWelldone!\nYou can move to the actual Path from the example!";
                }
            }
            else
            {
                text = "You lost!";
            }
            Console.WriteLine(text);
        }
    }
}
