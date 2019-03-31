using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Utility
{
    internal static class Render
    {
        #region Public Constructors

        static Render()
        {
            Console.CursorVisible = false;
        }

        #endregion Public Constructors

        #region Public Properties

        public static ConsoleColor Change_FGColor { get; set; } = ConsoleColor.DarkRed;
        // default Console Background Color and Default Color to change on cursor
        public static ConsoleColor Default_FGColor { get; set; } = ConsoleColor.Black;
        public static bool ForeGroundRender { get; set; } = true;
        public static bool Messages_Visible { get; set; } = true;
        // Describes the render path
        public static Path Renderpath { set; get; }

        #endregion Public Properties

        #region Public Methods

        public static void RenderMessages()
        {
            if (Messages_Visible)
            {
                int x = 0,
                    y = Renderpath.Devices.GetLength(1);
                if (Renderpath is ExamplePath)
                {
                    y += (Render.Renderpath as ExamplePath).Preview.Split('\n').Length + 1;
                }
                Console.SetCursorPosition(x, y);
                Console.Write(Game.Messages[Renderpath.LevelNumber - 1]);
                Console.SetCursorPosition(Game.Player.PositionY, Game.Player.PositionX);
            }
        }
        // render player and use the player positionx, positiony device of actual_path devices
        public static void RenderPlayer()
        {
            // setcursor to previus position and write the previous device where it is
            Console.SetCursorPosition(Game.Player.PreviousPositionY, Game.Player.PreviousPositionX);
            Console.Write(Renderpath.Devices[Game.Player.PreviousPositionX, Game.Player.PreviousPositionY].Character);
            // setcursor to current position and write the current device where it is
            Console.SetCursorPosition(Game.Player.PositionY, Game.Player.PositionX);
            char character = Convert.ToChar(Game.Player.Character.ToString());
            if (ForeGroundRender)
            {
                Console.BackgroundColor = Change_FGColor;
                character = Renderpath.Devices[Game.Player.PositionX, Game.Player.PositionY].Character;
            }
            Console.Write(character);
            Console.BackgroundColor = Default_FGColor;
            Console.SetCursorPosition(Game.Player.PositionY, Game.Player.PositionX);
            RenderMessages();
        }
        // Write the Actual Path but on x, y position write the player character
        public static void RenderRenderPath()
        {
            Console.Clear();
            for (int i = 0; i < Renderpath.Devices.GetLength(0); i++)
            {
                for (int j = 0; j < Renderpath.Devices.GetLength(1); j++)
                {
                    Console.Write(Renderpath.Devices[i, j].Character);
                }
                Console.WriteLine();
            }
            // if the path is Example Path then Write The Preview
            if (Renderpath is ExamplePath)
            {
                Console.WriteLine();
                Console.WriteLine((Renderpath as ExamplePath).Preview);
                Console.WriteLine();
            }
            RenderMessages();
            RenderPlayer();
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

        #endregion Public Methods
    }
}
