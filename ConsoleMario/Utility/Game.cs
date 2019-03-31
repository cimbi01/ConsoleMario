using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Devices;

namespace ConsoleMario.Utility
{
    static class Game
    {
        // List of messages by Pathlevel
        public static List<string> Messages { get; } = new List<string>();
        // Describes that player want to exit
        private static bool exited = false;
        // Describes the player
        public static Player Player { get; private set; }
        // Describes the max of levels of the player won
        public static int player_maxLevel = 0;
        // Describes the actual level
        private static int actual_level = 0;
        // Describes the actual path
        private static Path actual_path;
        // InitPlayer and add strings to messages by max_level
        static Game()
        {
            InitPlayer();
            for (int i = 0; i < player_maxLevel+1; i++)
            {
                AddNewMessageLine();
            }
        }
        // Init player by UP, DOWN, RIGHT, LEFT KEY and Init Render by MessagesVisible and RenderForground
        private static void InitPlayer()
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
            if (!CheckedDataInput.DecisionInput(input, ""))
            {
                input = "Would you like to change the default RenderType?:" +
                    "\nDefault RenderType: Console.ForeGroundColour" +
                    "\nThe Character is not shown only the foreground of the colour is different" +
                    "\nPress Enter if No or something then enter if Yes!";
                Render.ForeGroundRender = CheckedDataInput.DecisionInput(input, "");
                input = "Would you like to make the messages visible?:" +
                    "\nPress Enter if No or something then enter if Yes!";
                Render.Messages_Visible = !CheckedDataInput.DecisionInput(input, "");
                input = "Would you like to change the default Character?:" +
                    "\nDefault character: " + Player.DefaultCharacter +
                    "\nPress Enter if No or something then enter if Yes!";
                if (!CheckedDataInput.DecisionInput(input, ""))
                {
                    Player.DefaultCharacter = CheckedDataInput.InputChar<char>("Press preferred Character button");
                }
                input = "Would you like to change the default Buttons?:" +
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
            Player = new Player();
        }
        public static void Play()
        {
            actual_path = new Path(actual_level);
            while (!exited)
            {
                Player.Reset();
                Render.Renderpath = actual_path;
                if (!Player.ExamplePathWin && actual_path.ExamplePath != null)
                {
                    Render.Renderpath = actual_path.ExamplePath;
                }
                Render.RenderRenderPath();
                Move();
                if (Player.Win)
                {
                    HandleWin();
                }
                else
                {
                    Render.WriteWinMessage(false);
                }
                System.Threading.Thread.Sleep(1000);
                exited = Path.MaxLevel == player_maxLevel;
            }
            // if player want to exit or there is no more Level available
            Console.WriteLine("Game over");
        }
        // set player_maxlevel, actual_level, player.ExamplePathWin accroding to situation
        private static void HandleWin()
        {
            Render.WriteWinMessage();
            if (!(Render.Renderpath is ExamplePath))
            {
                if (actual_level == player_maxLevel && player_maxLevel < Path.MaxLevel)
                {
                    player_maxLevel++;
                    AddNewMessageLine();
                }
                if (actual_level < Path.MaxLevel)
                {
                    actual_level++;
                    try
                    {
                        actual_path = new Path(actual_level);
                    }
                    catch (NoMoreLevelException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                        exited = true;
                    }
                    Player.ExamplePathWin = false;
                }
            }
            else
            {
                Player.ExamplePathWin = true;
            }
        }
        private static void Move()
        {
            ConsoleKey ch = Console.ReadKey(true).Key;
            Player.Move(ch);
            AddMessage("Player moved x Direction: " + Convert.ToString(Player.PositionX - Player.PreviousPositionX) +
                    " y Direction: " + Convert.ToString(Player.PositionY - Player.PreviousPositionY));
            RenderPlayer();
            if (!Player.Win && Player.Life > 0)
            {
                Move();
            }
        }
        // render player and use the player positionx, positiony device of actual_path devices
        private static void RenderPlayer()
        {
            if (Player.Life > 0)
            {
                Render.RenderPlayer();
                System.Threading.Thread.Sleep(300);
                try
                {
                    AddMessage("Device: " + Convert.ToString(Render.Renderpath.Devices[Player.PositionX, Player.PositionY].GetType()));
                    Render.Renderpath.Devices[Player.PositionX, Player.PositionY].Use(Player);
                }
                // if the door is closed 
                catch (DoorIsClosedException)
                {
                    AddMessage("Run in Closed Door");
                    Player.RenderNeeded = false;
                    // step back and renderplayer again
                    Player.StepBack();
                    AddMessage("Player moved x Direction: " + Convert.ToString(Player.PositionX - Player.PreviousPositionX) +
                        " y Direction: " + Convert.ToString(Player.PositionY - Player.PreviousPositionY));
                    RenderPlayer();
                }
                // if run in wall
                catch (RunInWallException)
                {
                    AddMessage("Run in Wall");
                    Player.RenderNeeded = false;
                    // step back and renderplayer again
                    Player.StepBack();
                    AddMessage("Player moved x Direction: " + Convert.ToString(Player.PositionX - Player.PreviousPositionX) +
                        " y Direction: " + Convert.ToString(Player.PositionY - Player.PreviousPositionY));
                    RenderPlayer();
                }
                // if player need rerender
                if (Player.RenderNeeded)
                {
                    AddMessage("Player moved x Direction: " + Convert.ToString(Player.PositionX - Player.PreviousPositionX) +
                        " y Direction: " + Convert.ToString(Player.PositionY - Player.PreviousPositionY));
                    Player.RenderNeeded = false;
                    RenderPlayer();
                }
            }
        }
        // Add message to messages and write it under Path
        private static void AddMessage(string message)
        {
            Messages[actual_level] += message+ '\n';
            Render.RenderMessages();
        }
        // Add new Message string line to messages if new level available
        private static void AddNewMessageLine()
        {
            Messages.Add(Convert.ToString((player_maxLevel+1)) + ".level\n");
        }
    }
}
