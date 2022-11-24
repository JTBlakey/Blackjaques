// A game of BlackJack made in PURE unadulterated C#, made solely by Joel and BElijahmin <33333
// Copyright 2022
// <3
// <3
// <3

using System.Text;

namespace Blackjack2022;

class Program
{

    public static Settings settings = new Settings();

    //Main program
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8; // allow emojis

        try
        {
            try
            {
                settings = Settings.LoadSettingsFromFile(FileLib.SETTINGS_FILE_LOCATION);
            }
            catch
            {
                if (!Directory.Exists("./SETTINGS"))
                {
                    Directory.CreateDirectory("./SETTINGS");
                }

                settings = new Settings();
                Settings.SaveSettingsToFile(settings, FileLib.SETTINGS_FILE_LOCATION);
                Settings.LoadSettings(settings);
            }
        }
        catch
        {
            throw new FileLoadException("BlackJack cannot read/write files in its directory, please make sure it is in a directory it has the ability to edit");
        }

        bool doMenu = false;

        while (!doMenu)
        {
            doMenu = MainMenu();
        }

        Console.WriteLine("GoodBye!");
        Console.WriteLine("See you soon! (we hope you spend even more money next time)");
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
            Console.WriteLine("DEBUG");
            #endif

            Console.WriteLine("");
            Console.WriteLine("");
            
            int iChoice = Menu.NumberMenu(new string[]
            {
                "Play",
                "Rules",
                "Options",
                "Exit"
            });

            bool doIt = false;

            switch (iChoice)
            {
                case 1:
                    BJGame.BJTime();
                    break;
                case 2:
                    Rules();
                    break;
                case 3:
                    Options();
                    break;
                case 4:
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

            Console.WriteLine("1: Background color: " + settings.backgroundColor.ToString());
            Console.WriteLine("2: Foreground color: " + settings.foregroundColor.ToString());
            Console.WriteLine("3: Username:         " + settings.name);
            Console.WriteLine("4: Reset All");

            Console.WriteLine();
            Console.WriteLine("Enter M to return to menu.");

            ConsoleKeyInfo mForMenu = Console.ReadKey();

            if (mForMenu.Key == ConsoleKey.M)
            {
                Settings.SaveSettingsToFile(settings, FileLib.SETTINGS_FILE_LOCATION);

                return;
            }

            if (mForMenu.Key == ConsoleKey.D1)
                settings.backgroundColor = SelectConsoleColor(settings.backgroundColor);

            if (mForMenu.Key == ConsoleKey.D2)
                settings.foregroundColor = SelectConsoleColor(settings.foregroundColor);

            if (mForMenu.Key == ConsoleKey.D3)
                Console.WriteLine("FEATURE NOT ADDED YET");

            if (mForMenu.Key == ConsoleKey.D4)
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
}