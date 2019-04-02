using System;
using System.Collections.Generic;

namespace ConsoleMario.Utility
{
    internal static class Game
    {
        #region Public Constructors

        // InitPlayer and add strings to messages by max_level
        static Game()
        {
            Init.InitPlayer();
        }

        #endregion Public Constructors

        #region Public Properties

        // List of messages by Pathlevel
        public static List<string> Messages { get; } = new List<string>();
        // Describes the player
        public static Player Player { get; internal set; }

        #endregion Public Properties

        #region Public Methods

        // Add new Message string line to messages if new level available
        public static void AddNewMessageLine()
        {
            Messages.Add(Convert.ToString((Player.Max_Level + 1)) + ".level\n");
        }
        public static void Play()
        {
            while (!exited)
            {
                // Need to be reseted every turn because of the used elements
                Player.Actual_Path = new Path(Player.Actual_Level);
                Player.Reset();
                Render.Renderpath = Player.Actual_Path;
                bool actulpathisexample = !Player.ExamplePathWin;
                actulpathisexample = actulpathisexample && Player.Actual_Path.ExamplePath != null;
                if (actulpathisexample)
                {
                    Render.Renderpath = Player.Actual_Path.ExamplePath;
                }
                Render.RenderRenderPath();
                Move();
                Render.Initialized = false;
                if (Player.Win)
                {
                    HandleWin();
                }
                else
                {
                    Render.WriteWinMessage(false);
                }
                System.Threading.Thread.Sleep(1000);
                exited = Path.MaxLevel == Player.Max_Level;
            }
            // if player want to exit or there is no more Level available
            Console.WriteLine("Game over");
        }

        #endregion Public Methods

        #region Private Fields

        // Describes that player want to exit
        private static bool exited = false;

        #endregion Private Fields

        #region Private Methods

        // Add message to messages and write it under Path
        private static void AddMessage(string message)
        {
            Messages[Player.Actual_Level] += message + '\n';
            Render.RenderMessages();
        }
        // set player_maxlevel, actual_level, player.ExamplePathWin accroding to situation
        private static void HandleWin()
        {
            Render.WriteWinMessage();
            if (!(Render.Renderpath.IsExample))
            {
                if (Player.Actual_Level == Player.Max_Level && Player.Max_Level < Path.MaxLevel)
                {
                    Player.Max_Level++;
                    AddNewMessageLine();
                }
                if (Player.Actual_Level < Path.MaxLevel)
                {
                    Player.Actual_Level++;
                    IncreaseActualPath();
                    Player.ExamplePathWin = false;
                }
            }
            else
            {
                Player.ExamplePathWin = true;
            }
        }
        private static void IncreaseActualPath()
        {
            try
            {
                Player.Actual_Path = new Path(Player.Actual_Level);
            }
            catch (Exceptions.UtilityExceptions.NoMoreLevelException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                exited = true;
            }
        }
        private static void Move()
        {
            ConsoleKey ch = Console.ReadKey(true).Key;
            Player.Move(ch);
            AddMessage("Player moved x Direction: " + Convert.ToString(Player.PositionX - Player.PreviousPositionX) +
                    " y Direction: " + Convert.ToString(Player.PositionY - Player.PreviousPositionY));
            PlayerUseDeviceOnPosition();
            if (!Player.Win && Player.Life > 0)
            {
                Move();
            }
        }
        // render player and use the player positionx, positiony device of actual_path devices
        private static void PlayerUseDeviceOnPosition()
        {
            if (Player.Life > 0)
            {
                Player.RenderNeeded = false;
                Render.RenderPlayer();
                System.Threading.Thread.Sleep(300);
                AddMessage("Device: " + Convert.ToString(Render.Renderpath.Devices[Player.PositionX, Player.PositionY].GetType()));
                try
                {
                    Render.Renderpath.Devices[Player.PositionX, Player.PositionY].Use(Player);
                }
                // if the door is closed
                catch (Exceptions.DeviceExceptions.DoorIsClosedException)
                {
                    Player.Life--;
                    AddMessage("Run in Closed Door");
                    // step back and renderplayer again
                    Player.StepBack();
                    AddMessage("Player moved x Direction: " + Convert.ToString(Player.PositionX - Player.PreviousPositionX) +
                        " y Direction: " + Convert.ToString(Player.PositionY - Player.PreviousPositionY));
                }
                // if run in wall
                catch (Exceptions.DeviceExceptions.RunInWallException)
                {
                    AddMessage("Run in Wall");
                    // step back and renderplayer again
                    Player.StepBack();
                    AddMessage("Player moved x Direction: " + Convert.ToString(Player.PositionX - Player.PreviousPositionX) +
                        " y Direction: " + Convert.ToString(Player.PositionY - Player.PreviousPositionY));
                }
                // if player need rerender
                if (Player.RenderNeeded)
                {
                    PlayerUseDeviceOnPosition();
                }
            }
        }

        #endregion Private Methods
    }
}
