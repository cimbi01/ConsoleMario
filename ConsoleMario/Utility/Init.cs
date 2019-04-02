using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Utility
{
    internal static class Init
    {
        private static void InitButtons()
        {
            string input = "Would you like to change the default Buttons?:" +
                    "\nRIGHT button: " + Player.RIGHT +
                    "\nLEFT button: " + Player.LEFT +
                    "\nUP button: " + Player.UP +
                    "\nDOWN button: " + Player.DOWN +
                    "\nPress Enter if No or something then enter if Yes!";
            if (!CheckedDataInput.DecisionInput(input, ""))
            {
                Player.UP = CheckedDataInput.InputChar<ConsoleKey>("Press preferred UP button");
                Player.DOWN = CheckedDataInput.InputChar<ConsoleKey>("Press preferred DOWN button");
                Player.RIGHT = CheckedDataInput.InputChar<ConsoleKey>("Press preferred RIGHT button");
                Player.LEFT = CheckedDataInput.InputChar<ConsoleKey>("Press preferred LEFT button");
            }
        }
        private static void InitDefaultCharacter()
        {
            string input = "Would you like to change the default Character?:" +
                    "\nDefault character: " + Player.DefaultCharacter +
                    "\nPress Enter if No or something then enter if Yes!";
            if (!CheckedDataInput.DecisionInput(input, ""))
            {
                Player.DefaultCharacter = CheckedDataInput.InputChar<char>("Press preferred Character button");
            }
        }
        private static bool InitDefaults()
        {
            // Datainput
            string input = "Would you like to change the default settings:" +
                            "\nRIGHT button: " + Player.RIGHT +
                            "\nLEFT button: " + Player.LEFT +
                            "\nUP button: " + Player.UP +
                            "\nDOWN button: " + Player.DOWN +
                            "\nDefault character: " + Player.DefaultCharacter +
                            "\nCharacter: unvisible" +
                            "\nStep Cursor ForeGroundColor: " + Render.Change_FGColor +
                            "Messages Visible: " + Render.Messages_Visible +
                            "\nPress Enter if No or something then enter if Yes!";
            return CheckedDataInput.DecisionInput(input, "");
        }
        private static void InitMessagesVisible()
        {
            string input = "Would you like to make the messages visible?:" +
                "\nPress Enter if No or something then enter if Yes!";
            Render.Messages_Visible = !CheckedDataInput.DecisionInput(input, "");
        }
        private static void InitRenderType()
        {
            string input = "Would you like to change the default RenderType?:" +
                "\nDefault RenderType: Console.ForeGroundColour" +
                "\nThe Character is not shown only the foreground of the colour is different" +
                "\nPress Enter if No or something then enter if Yes!";
            Render.ForeGroundRender = CheckedDataInput.DecisionInput(input, "");
        }

        // Init player by UP, DOWN, RIGHT, LEFT KEY and Init Render by MessagesVisible and RenderForground
        public static void InitPlayer()
        {
            if (!InitDefaults())
            {
                InitButtons();
                InitDefaultCharacter();
                InitMessagesVisible();
                InitRenderType();
            }
            Game.Player = new Player();
            for (int i = 0; i < Game.Player.Max_Level + 1; i++)
            {
                Game.AddNewMessageLine();
            }
        }
    }
}
