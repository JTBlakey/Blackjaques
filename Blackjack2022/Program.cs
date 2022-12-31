// A game of BlackJack made in PURE unadulterated C#, made solely by Joel and BElijahmin <33333
// Copyright 2022
// <3
// <3
// <3

using System.Text;

namespace Blackjack2022;

class Program
{
    public static Settings? settings = new Settings();
    public static Player? player = new Player();
    public static Stats? stats = new Stats();

    public static bool debugPlusPlus = false;

    //Main program
    static void Main(string[] args)
    {
        #if DEBUG
        foreach (string arg in args)
            if (arg.ToUpper() == "DEBUG++")
                debugPlusPlus = true;
        #endif

        Console.OutputEncoding = Encoding.UTF8; // allow emojis

        if (!Directory.Exists("./DATA"))
        {
            Directory.CreateDirectory("./DATA");
        }

        if (!File.Exists(FileLib.GetFullAddress(FileLib.SETTINGS_FILE_LOCATION)))
        {
            if (File.Exists(FileLib.GetFullAddress(FileLib.OLD_SETTINGS_FILE_LOCATION)))
            {
                settings = FileLib.Open<Settings>(FileLib.OLD_SETTINGS_FILE_LOCATION);
                FileLib.Save<Settings>(settings, FileLib.SETTINGS_FILE_LOCATION);

                File.Delete(FileLib.GetFullAddress(FileLib.OLD_SETTINGS_FILE_LOCATION));
                Directory.Delete("./SETTINGS");
            }
        }

        settings = FileLib.Open<Settings>(FileLib.SETTINGS_FILE_LOCATION);
        player = FileLib.Open<Player>(FileLib.PLAYER_FILE_LOCATION);
        stats = FileLib.Open<Stats>(FileLib.STATS_FILE_LOCATION);

        if (settings == null)
            settings = new Settings();

        if (player == null)
            player = new Player();

        if (stats == null)
            stats = new Stats();

        Settings.LoadSettings(settings);

        bool doMenu = false;

        while (!doMenu)
        {
            doMenu = MainMenu();
        }

        Settings.SaveSettingsToFile(settings, FileLib.SETTINGS_FILE_LOCATION);
        Player.SavePlayerToFile(player, FileLib.PLAYER_FILE_LOCATION);
        Stats.SaveStatsToFile(stats, FileLib.STATS_FILE_LOCATION);

        Console.WriteLine("\n\nGoodBye!");
        Console.WriteLine("\nSee you soon! (we hope you spend even more money next time)");
    }

    //Menu
    static bool MainMenu() // i <3 bj
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("  ######   ###                    ###        ####                 ###   (c) 2022");
            Console.WriteLine("  ##  ##   ##                     ##         ##                   ##");
            Console.WriteLine("  ##  ##   ##     ####    ####    ##  ##     ##   ####    ####    ##  ##");
            Console.WriteLine("  #####    ##        ##  ##  ##   ## ##      ##      ##  ##  ##   ## ##");
            Console.WriteLine("  ##  ##   ##     #####  ##       ####   ##  ##   #####  ##       ####");
            Console.WriteLine("  ##  ##   ##    ##  ##  ##  ##   ## ##  ##  ##  ##  ##  ##  ##   ## ##");
            Console.WriteLine("  ######   ####    ### ##  ####   ###  ##  ####    ### ##  ####   ###  ##");

            #if DEBUG
            Console.WriteLine("DEBUG" + (debugPlusPlus ? "++" : "")); // make sure people know that they running debug version (should probably show build no aswell)
            #endif

            Console.WriteLine("");
            Console.WriteLine("");
            
            int iChoice = Menu.NumberMenu(new string[]
            {
                "Play",
                "Rules",
                "Options",
                "Stats",
                "Exit"
            });

            switch (iChoice)
            {
                case 1:
                    player.money = BJGame.BJTime(player.money);
                    break;
                case 2:
                    Rules();
                    break;
                case 3:
                    Options();
                    break;
                case 4:
                    Statisics();
                    break;
                case 5:
                    return true;
                default:
                    break;
            }
        }
    }

    //Subroutine to output options
    static void Rules()
    {
        using (StreamReader sr = new StreamReader(FileLib.GetFullAddress(FileLib.RULES_FILE_LOCATION)))
        {
            while (sr.Peek() >= 0)
            {
                Console.WriteLine(sr.ReadLine());
            }
        }

        Console.WriteLine();
        Console.WriteLine("Enter M to return to menu.");

        while (true)
        {
            ConsoleKeyInfo mForMenu = Console.ReadKey();

            if (mForMenu.Key == ConsoleKey.M)
                return;
        }
    }

    static void Options()
    {
        /*Console.WriteLine("Choice is merely an illusion.");
        Console.WriteLine("However in our upcoming 49$ expansion pack, you can purchase free will");
        Console.WriteLine("Please enter your credit card details below to prepurchase for the LOW LOW price of $38.99*");
        Console.WriteLine("* not including the $69.99 prepurchase fee :)");*/ // i know, its sad to see this go

        while (true)
        {
            Settings.LoadSettings(settings);

            Console.Clear();

            int option = Menu.NumberMenu(new string[]
            {
                "Background color: " + settings.backgroundColor.ToString(),
                "Foreground color: " + settings.foregroundColor.ToString(),
                "Reset All"
            }, "", ConsoleKey.M);

            if (option == -1)
            {
                Settings.SaveSettingsToFile(settings, FileLib.SETTINGS_FILE_LOCATION);

                return;
            }

            if (option == 1)
                settings.backgroundColor = SelectConsoleColor(settings.backgroundColor);

            if (option == 2)
                settings.foregroundColor = SelectConsoleColor(settings.foregroundColor);

            if (option == 4)
                settings = new Settings();
        }
    }

    static ConsoleColor SelectConsoleColor(ConsoleColor original = ConsoleColor.Black)
    {
        ConsoleColor[] allColors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

        int selected = -1;

        foreach (ConsoleColor someColors in allColors)
        {
            selected++;

            if (someColors == original)
                break;
        }

        while (true)
        {
            if (allColors[selected] == ConsoleColor.Black)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }


            Console.ForegroundColor = allColors[selected];

            Console.Clear();

            Console.WriteLine(allColors[selected].ToString());

            ConsoleKey inputKey = Console.ReadKey().Key;

            if (inputKey == ConsoleKey.Enter)
                return allColors[selected];

            if (inputKey == ConsoleKey.LeftArrow)
                selected--;

            if (inputKey == ConsoleKey.RightArrow)
                selected++;


            if (selected < 0)
                selected = allColors.Length - 1;

            if (selected >= allColors.Length)
                selected = 0;
        }
    }

    static void Statisics()
    {
        Console.Clear();

        Console.WriteLine("Games Played: " + stats.gamesPlayed.ToString());
        Console.WriteLine("Money Made: " + stats.moneyMade.ToString());
        Console.WriteLine("Money Lost: " + stats.moneyLost.ToString());
        Console.WriteLine("Total Cards Hit: " + stats.totalCardsHit.ToString());
        Console.WriteLine("Average Cards Hit: " + stats.averageCardsHit.ToString());
        Console.WriteLine("Total Win: " + stats.totalWin.ToString());
        Console.WriteLine("Total Loss: " + stats.totalLoss.ToString());
        Console.WriteLine("Win Chance: " + stats.winChance.ToString());

        Console.WriteLine();
        Console.WriteLine("Enter M to return to menu.");

        while (true)
        {
            ConsoleKeyInfo mForMenu = Console.ReadKey();

            if (mForMenu.Key == ConsoleKey.M)
                return;
        }
    }

    //DONT TOUCH ANYTHING BELOW THIS LINE OR GOD WILL SMIGHT YOU
    //SMITE*
    // lmao you nerds really care about spelling lol
    // <3
    // <3
    // yes we do now get back to work, no toilet breaks
    // ifg (wot dis mene) you need to go, please use the bottle


    // TOP TEXT

    // FUNNY IMAGE

    // BOTTOM TEXT

    // This post was made by Javascript gang
    // THIS WAS MADE BY THE JAVA GANG LMAO (we think were so funny) 8==D
    // mfs the type to say ROFL out loud
    // mfs write comments instead of coding lmao
    // mfs who say LMAO out loud "luh-maohw"

    // OH NO, WHERE DID THE CODE GO
    // where did the code go?
    // oh its in separate files now
}