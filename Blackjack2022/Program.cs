// A game of BlackJack made in PURE unadulterated C#, made solely by Joel and BElijahmin <33333
// Copyright 2022
// <3
// <3

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.IO;
using System;
using System.Text;

namespace Blackjack2022;

class Program
{
    public const string SETTINGS_FILE_LOCATION = "./SETTINGS/settings.json";

    public static Settings settings;

    public class Settings
    {

        public ConsoleColor foregroundColor { get; set; }
        public ConsoleColor backgroundColor { get; set; }

        public string name { get; set; }

        public Settings()
        {
            name = "Username";

            foregroundColor = ConsoleColor.White;
            backgroundColor = ConsoleColor.Black;
        }

        public Settings(string sName, ConsoleColor foreground, ConsoleColor background)
        {
            name = sName;
            foregroundColor = foreground;
            backgroundColor = background;
        }

        public void SwapColors()
        {
            ConsoleColor temp = foregroundColor;

            foregroundColor = backgroundColor;
            backgroundColor = temp;
        }
    }

    public static void SaveSettingsToFile(Settings settings, string file)
    {
        string data = SaveSettings(settings);

        if (data == null)
            throw new InsufficientExecutionStackException("BOOP!");

        using (StreamWriter sw = new StreamWriter(file))
        {
            sw.WriteLine(data);
        }
    }

    public static string SaveSettings(Settings settings)
    {
        return JsonSerializer.Serialize(settings);
    }

    public static Settings LoadSettingsFromFile(string file)
    {
        string fileContents = string.Join("\n", System.IO.File.ReadAllLines(file));

        Settings settings = JsonSerializer.Deserialize<Settings>(fileContents);

        LoadSettings(settings);

        return settings;
    }

    public static void LoadSettings(Settings settings)
    {
        Console.BackgroundColor = settings.backgroundColor;
        Console.ForegroundColor = settings.foregroundColor;
    }

    //Main program
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8; // allow emojis

        try
        {
            settings = LoadSettingsFromFile(SETTINGS_FILE_LOCATION);
        }
        catch
        {
            if (!Directory.Exists("./SETTINGS"))
            {
                Directory.CreateDirectory("./SETTINGS");
            }
            
            settings = new Settings();
            SaveSettingsToFile(settings, SETTINGS_FILE_LOCATION);
            LoadSettings(settings);
        }

        bool doMenu = false;

        while (!doMenu)
        {
            doMenu = Menu();
        }
    }

    //Menu
    static bool Menu() // i <3 bj
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

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("[1. Play    ]");
            Console.WriteLine("[2. Rules   ]");
            Console.WriteLine("[3. Options ]");
            Console.WriteLine("[4. Exit    ]");
            Console.WriteLine("");
            Console.WriteLine("Enter an option number: ");

            string choice = Console.ReadLine();
            int iChoice = 0;
            bool doIt = false;

            try
            {
                iChoice = Convert.ToInt16(choice);
                doIt = true;
            }
            catch {}

            if (doIt)
            {
                switch (Convert.ToInt16(iChoice))
                {
                    case 1:
                        BJTime();
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
    }

    //Subroutine to output options
    static void Rules()
    {
        string ruleLoc = "./Ass/Rules.txt";

        using (StreamReader sr = new StreamReader(ruleLoc))
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

        Console.WriteLine("1: Background color: " + settings.backgroundColor.ToString());
        Console.WriteLine("2: Foreground color: " + settings.foregroundColor.ToString());
        Console.WriteLine("3: Username:         " + settings.name);

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
    
    public static int[] Game()
    {
        List<Card> deck = Card.Deck().ToList<Card>();

        Random rng = new Random();

        for (int i = 0; i < rng.Next(40, 99); i++) { rng.Next(); } // randomize the random

        deck = deck.OrderBy(x => rng.Next()).ToList<Card>(); // shuffle

        List<Card> player1 = new List<Card>();
        List<Card> player2 = new List<Card>();

        for (int i = 0; i < 2; i++)
        {
            player1.Add(deck[0]);
            deck.RemoveAt(0);

            player2.Add(deck[0]);
            deck.RemoveAt(0);
        }

        bool stop = false;

        do
        {
            Console.Clear();

            Console.WriteLine("COM:");
            OutputCardArray(player2.ToArray(), 1);
            Console.WriteLine("YOU:");
            OutputCardArray(player1.ToArray());
            Console.WriteLine();
            Console.Write("HIT? ");

            string playerIn = Console.ReadLine();
            Console.WriteLine();

            if (playerIn.ToUpper() == "Y")
            {
                player1.Add(deck[0]);
                deck.RemoveAt(0);
            }
            else if (playerIn.ToUpper() == "N")
            {
                stop = true;
            }
        }
        while (!stop && (Card.Score(player1.ToArray()) <= 21)); // loverly logic (much better than it was)

        Console.Clear();

        while (Card.Score(player2.ToArray()) <= 21 && (Card.Score(player1.ToArray()) > 21 || Card.Score(player1.ToArray()) < Card.Score(player2.ToArray()))) // even better logic
        {
            player2.Add(deck[0]);
            deck.RemoveAt(0);
        }

        Console.WriteLine("COM:");
        OutputCardArray(player2.ToArray());
        Console.WriteLine("YOU:");
        OutputCardArray(player1.ToArray());

        int score = GetWinner(Card.Score(player1.ToArray()), Card.Score(player2.ToArray()));

        if (score == 0) // draw = dealer win
            score = 2;

        return new int[] { score , Card.Score(player1.ToArray()) , Card.Score(player2.ToArray()) };
    }

    public static void OutputCardArray(Card[] chards, uint show = 0)
    {
        string pout = "";

        string[] chardID = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        int i = 1;

        foreach (Card chard in chards)
        {
            if (i > show && show > 0)
            {
                pout += chard.GetSuiteChar() + "? ";
            }
            else
            {
                pout += chard.GetSuiteChar() + chardID[chard.num] + " ";
            }

            i++;
        }

        Console.WriteLine(pout);
    }

    static void BJTime()
    {
        int score = 0;

        while (true)
        {
            int[] gs = Game();
            score += gs[0];

            Console.WriteLine();
            Console.WriteLine(gs[0] == 1 ? "you won!" : "you lost :(");
            Console.WriteLine();
            Console.WriteLine("the dealers cards where worth: " + gs[2].ToString());
            Console.WriteLine("your cards where worth: " + gs[1].ToString());
            Console.WriteLine();
            Console.WriteLine("Score: " + score.ToString());
            Console.ReadLine();
        }
    }

    public static int GetWinner(int score1, int score2) // 0 - noone, 1 - score1, 2 - score2
    {
        if (score1 > 21 && score2 > 21) // both over limit
            return 0;

        if (score1 > 21) // score1 over limit
            return 2;

        if (score2 > 21) // score2 over limit
            return 1;

        if (score1 == score2) // equal
            return 0;

        if (score1 > score2) // score1 LETSSS GOOO
            return 1;
        else
            return 2;
    }

    public class Card
    {
        public static string[] suiteChar = { "♣", "♠", "♥", "♦" };

        public int num;
        public int suite;

        public Card(string Isuite, int Inum)
        {
            switch (Isuite.ToUpper())
            {
                case "CLUBS":
                    suite = 0;
                    break;

                case "SPADES":
                    suite = 1;
                    break;

                case "HEARTS":
                    suite = 2;
                    break;

                case "DIAMONDS":
                    suite = 3;
                    break;
            }

            num = Inum;
        }

        public Card(int Isuite, int Inum)
        {
            suite = Isuite;
            num = Inum;
        }

        public string GetSuite()
        {
            switch (suite)
            {
                case 0:
                    return "CLUBS";

                case 1:
                    return "SPADES";

                case 2:
                    return "HEARTS";

                default:
                    return "DIAMONDS";
            }
        }

        public string GetSuiteChar()
        {
            return (string)suiteChar[suite];
        }

        public static Card[] Deck()
        {
            Card[] deck = new Card[52];

            int i = 0;

            for (int suite = 0; suite < 4; suite++)
            {
                for (int num = 0; num < 13; num++)
                {
                    deck[i++] = new Card(suite, num);
                }
            }

            return deck;
        }

        public static int Score(Card[] chards)
        {
            int acesOHYEAH = 0;
            int score = 0;

            for (int i = 0; i < chards.Length; i++)
            {
                if (chards[i].num >= 1) // not an ace
                {
                    if (chards[i].num >= 10) // is a symbol card
                    {
                        score += 10;
                    }
                    else // die nerd
                    {
                        score += chards[i].num + 1;
                    }
                }
                else // ACE!
                {
                    acesOHYEAH++; // OHYEAH
                }
            }

            if (acesOHYEAH == 0)
                return score;

            if (score + (acesOHYEAH * 11) > 21)
                return score + (acesOHYEAH * 1);

            return score + (acesOHYEAH * 11);
        }//we are very productive at bj's
    }
}