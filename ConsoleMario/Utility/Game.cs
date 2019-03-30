using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Devices;

namespace ConsoleMario
{
    static class Game
    {
        // List of messages by Pathlevel
        public static List<string> Messages { get; } = new List<string>();
        // Describes that player want to exit
        private static bool exited = false;
        // Describes all the existed Paths returned by path.init
        private static readonly List<Path> paths = Path.Init();
        // Describes the player
        public static Player Player { get; private set; }
        // Describes the max of levels of the player won
        public static int player_maxLevel = 0;
        // Describes the actual level
        private static int actual_level = 0;
        // Describes the actual path
        private static Path actual_path;
        static Game()
        {
            InitPlayer();
            for (int i = 0; i < player_maxLevel+1; i++)
            {
                AddNewMessageLine();
            }
        }
        private static void InitPlayer()
        {
            // Datainput
            string input = "Would you like to change the default settings:" +
                "\nCharacter: unvisible" +
                "\nStep cursor ForeGroundColor " + Render.Change_FGColor +
                "\nDefault ForeGroundColor " + Render.Default_FGColor +
                "\nPress Enter if yes!";
            if (CheckedDataInput.DecisionInput(input, ""))
            {
                Console.Clear();
                /*
                input = "Would you like to change the default Buttons?:" +
                    "\nRIGHT button: " + Player.RIGHT +
                    "\nLEFT button: " + Player.LEFT +
                    "\nUP button: " + Player.UP +
                    "\nDOWN button: " + Player.DOWN +
                    "\nPress Enter if yes!";
                if (CheckedDataInput.DecisionInput(input, ""))
                {
                    Console.Clear();
                    // ERROR?
                    // HOW TO CONVERT FROM KEYCHAR TO CHAR
                    Player.UP = CheckedDataInput.InputChar("Press preferred UP button");
                    Player.DOWN = CheckedDataInput.InputChar("Press preferred DOWN button");
                    Player.RIGHT = CheckedDataInput.InputChar("Press preferred RIGHT button");
                    Player.LEFT = CheckedDataInput.InputChar("Press preferred LEFT button");
                }
                */
                Console.Clear();
                input = "Would you like to change the default RenderType?:" +
                    "\nDefault RenderType: Console.ForeGroundColour" +
                    "\nThe Character is not shown only the foreground of the colour is different" +
                    "\nPress Enter if yes!";
                if (CheckedDataInput.DecisionInput(input, ""))
                {
                    Console.Clear();
                    Render.ForeGroundRender = false;
                    /* ERROR: CONSOLEKEY TO CHAR?
                    input = "Would you like to change the default Character?:" +
                        "\nDefault character: " + Player.DefaultCharacter +
                        "\nPress Enter if yes!";
                    if (CheckedDataInput.DecisionInput(input, ""))
                    {
                        Console.Clear();
                        Player.DefaultCharacter = CheckedDataInput.InputChar("Press preferred Character button");
                    }
                    */
                }
            }
            Player = new Player();
        }
        public static void Play()
        {
            while (!exited)
            {
                Player.Reset();
                actual_path = new Path(paths[actual_level]);
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
            char ch = Console.ReadKey(true).KeyChar;
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
        public static void AddMessage(string message)
        {
            Messages[actual_level] += message+ '\n';
            Render.RenderMessages();
        }
        public static void AddNewMessageLine()
        {
            Messages.Add(Convert.ToString((player_maxLevel+1)) + ".level\n");
        }
    }
}
