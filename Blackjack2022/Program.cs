// A game of BlackJack made in PURE unadulterated C#, made solely by Joel and BElijahmin <33333
// Copyright 2022
// <3

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.IO;
using System;

namespace Blackjack2022;

class Program
{
    public const string SETTINGS_FILE_LOCATION = "SETTINGS/settings.json";
    public static string username;

    public class Settings
    {

        public ConsoleColor foregroundColor;
        public ConsoleColor backgroundColor;

        public string name;

        public Settings()
        {
            name = "Username";

            foregroundColor = ConsoleColor.White;
            backgroundColor = ConsoleColor.Blue;
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

    public static string GetCurrentDir()
    {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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

    public static void LoadSettingsFromFile(string file)
    {
        string fileContents = string.Join("\n", System.IO.File.ReadAllLines(GetCurrentDir() + file));

        Settings settings = JsonSerializer.Deserialize<Settings>(fileContents);

        LoadSettings(settings);
    }

    public static void LoadSettings(Settings settings)
    {
        Console.BackgroundColor = settings.backgroundColor;
        Console.ForegroundColor = settings.foregroundColor;

        username = settings.name;
    }

    //Main program
    static void Main(string[] args)
    {
        /*try
        {
            LoadSettingsFromFile(SETTINGS_FILE_LOCATION);
        }
        catch
        {
            Settings settings = new Settings();
            SaveSettingsToFile(settings, SETTINGS_FILE_LOCATION);
            LoadSettings(settings);
        }*/

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

            Console.WriteLine("######   ###                    ###        ####                 ###   (c) 2022");
            Console.WriteLine("##  ##   ##                     ##         ##                   ##");
            Console.WriteLine("##  ##   ##     ####    ####    ##  ##     ##   ####    ####    ##  ##");
            Console.WriteLine("#####    ##        ##  ##  ##   ## ##      ##      ##  ##  ##   ## ##");
            Console.WriteLine("##  ##   ##     #####  ##       ####   ##  ##   #####  ##       ####");
            Console.WriteLine("##  ##   ##    ##  ##  ##  ##   ## ##  ##  ##  ##  ##  ##  ##   ## ##");
            Console.WriteLine("######   ####    ### ##  ####   ###  ##  ####    ### ##  ####   ###  ##");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("[1. Play    ]");
            Console.WriteLine("[2. Rules   ]");
            Console.WriteLine("[3. Options ]");
            Console.WriteLine("[4. Exit    ]");
            Console.WriteLine("");
            Console.WriteLine("Enter an option number: ");

            string Choice = Console.ReadLine();

            try
            {
                switch (Convert.ToInt16(Choice))
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
            catch {}
        }
    }

    //Subroutine to output options
    static void Rules()
    {
        Console.WriteLine("Equally well known as Twenty-One. The rules are simple, the play is thrilling, and there is opportunity for high strategy. In fact, for the expert player who mathematically plays a perfect game and is able to count cards, the odds are sometimes in that player's favor to win.\r\n\r\nBut even for the casual participant who plays a reasonably good game, the casino odds are less, making Blackjack one of the most attractive casino games for the player. While the popularity of Blackjack dates from World War I, its roots go back to the 1760s in France, where it is called Vingt-et-Un (French for 21). Today, Blackjack is the one card game that can be found in every American casino. As a popular home game, it is played with slightly different rules. In the casino version, the house is the dealer (a \"permanent bank\"). In casino play, the dealer remains standing, and the players are seated. The dealer is in charge of running all aspects of the game, from shuffling and dealing the cards to handling all bets. In the home game, all of the players have the opportunity to be the dealer (a \"changing bank\").\r\n\r\nThe Pack\r\nThe standard 52-card pack is used, but in most casinos several decks of cards are shuffled together. The six-deck game (312 cards) is the most popular. In addition, the dealer uses a blank plastic card, which is never dealt, but is placed toward the bottom of the pack to indicate when it will be time for the cards to be reshuffled. When four or more decks are used, they are dealt from a shoe (a box that allows the dealer to remove cards one at a time, face down, without actually holding one or more packs).\r\n\r\nObject of the Game\r\nEach participant attempts to beat the dealer by getting a count as close to 21 as possible, without going over 21.\r\n\r\nCard Values/scoring\r\nIt is up to each individual player if an ace is worth 1 or 11. Face cards are 10 and any other card is its pip value.\r\n\r\nBetting\r\nBefore the deal begins, each player places a bet, in chips, in front of them in the designated area. Minimum and maximum limits are established on the betting, and the general limits are from $2 to $500.\r\n\r\nThe Shuffle and Cut\r\nThe dealer thoroughly shuffles portions of the pack until all the cards have been mixed and combined. The dealer designates one of the players to cut, and the plastic insert card is placed so that the last 60 to 75 cards or so will not be used. (Not dealing to the bottom of all the cards makes it more difficult for professional card counters to operate effectively.)\r\n\r\nThe Deal\r\nWhen all the players have placed their bets, the dealer gives one card face up to each player in rotation clockwise, and then one card face up to themselves. Another round of cards is then dealt face up to each player, but the dealer takes the second card face down. Thus, each player except the dealer receives two cards face up, and the dealer receives one card face up and one card face down. (In some games, played with only one deck, the players' cards are dealt face down and they get to hold them. Today, however, virtually all Blackjack games feature the players' cards dealt face up on the condition that no player may touch any cards.)\r\n\r\nNaturals\r\nIf a player's first two cards are an ace and a \"ten-card\" (a picture card or 10), giving a count of 21 in two cards, this is a natural or \"blackjack.\" If any player has a natural and the dealer does not, the dealer immediately pays that player one and a half times the amount of their bet. If the dealer has a natural, they immediately collect the bets of all players who do not have naturals, (but no additional amount). If the dealer and another player both have naturals, the bet of that player is a stand-off (a tie), and the player takes back his chips.\r\n\r\nIf the dealer's face-up card is a ten-card or an ace, they look at their face-down card to see if the two cards make a natural. If the face-up card is not a ten-card or an ace, they do not look at the face-down card until it is the dealer's turn to play.\r\n\r\nThe Play\r\nThe player to the left goes first and must decide whether to \"stand\" (not ask for another card) or \"hit\" (ask for another card in an attempt to get closer to a count of 21, or even hit 21 exactly). Thus, a player may stand on the two cards originally dealt to them, or they may ask the dealer for additional cards, one at a time, until deciding to stand on the total (if it is 21 or under), or goes \"bust\" (if it is over 21). In the latter case, the player loses and the dealer collects the bet wagered. The dealer then turns to the next player to their left and serves them in the same manner.\r\n\r\nThe combination of an ace with a card other than a ten-card is known as a \"soft hand,\" because the player can count the ace as a 1 or 11, and either draw cards or not. For example with a \"soft 17\" (an ace and a 6), the total is 7 or 17. While a count of 17 is a good hand, the player may wish to draw for a higher total. If the draw creates a bust hand by counting the ace as an 11, the player simply counts the ace as a 1 and continues playing by standing or \"hitting\" (asking the dealer for additional cards, one at a time).\r\n\r\nThe Dealer's Play\r\nWhen the dealer has served every player, the dealers face-down card is turned up. If the total is 17 or more, it must stand. If the total is 16 or under, they must take a card. The dealer must continue to take cards until the total is 17 or more, at which point the dealer must stand. If the dealer has an ace, and counting it as 11 would bring the total to 17 or more (but not over 21), the dealer must count the ace as 11 and stand. The dealer's decisions, then, are automatic on all plays, whereas the player always has the option of taking one or more cards.\r\n\r\nSignaling Intentions\r\nWhen a player's turn comes, they can say \"Hit\" or can signal for a card by scratching the table with a finger or two in a motion toward themselves, or they can wave their hand in the same motion that would say to someone \"Come here!\" When the player decides to stand, they can say \"Stand\" or \"No more,\" or can signal this intention by moving their hand sideways, palm down and just above the table.\r\n\r\nSplitting Pairs\r\nIf a player's first two cards are of the same denomination, such as two jacks or two sixes, they may choose to treat them as two separate hands when their turn comes around. The amount of the original bet then goes on one of the cards, and an equal amount must be placed as a bet on the other card. The player first plays the hand to their left by standing or hitting one or more times; only then is the hand to the right played. The two hands are thus treated separately, and the dealer settles with each on its own merits. With a pair of aces, the player is given one card for each ace and may not draw again. Also, if a ten-card is dealt to one of these aces, the payoff is equal to the bet (not one and one-half to one, as with a blackjack at any other time).\r\n\r\nDoubling Down\r\nAnother option open to the player is doubling their bet when the original two cards dealt total 9, 10, or 11. When the player's turn comes, they place a bet equal to the original bet, and the dealer gives the player just one card, which is placed face down and is not turned up until the bets are settled at the end of the hand. With two fives, the player may split a pair, double down, or just play the hand in the regular way. Note that the dealer does not have the option of splitting or doubling down.\r\n\r\nInsurance\r\nWhen the dealer's face-up card is an ace, any of the players may make a side bet of up to half the original bet that the dealer's face-down card is a ten-card, and thus a blackjack for the house. Once all such side bets are placed, the dealer looks at the hole card. If it is a ten-card, it is turned up, and those players who have made the insurance bet win and are paid double the amount of their half-bet - a 2 to 1 payoff. When a blackjack occurs for the dealer, of course, the hand is over, and the players' main bets are collected - unless a player also has blackjack, in which case it is a stand-off. Insurance is invariably not a good proposition for the player, unless they are quite sure that there are an unusually high number of ten-cards still left undealt.\r\n\r\nSettlement\r\nA bet once paid and collected is never returned. Thus, one key advantage to the dealer is that the player goes first. If the player goes bust, they have already lost their wager, even if the dealer goes bust as well. If the dealer goes over 21, the dealer pays each player who has stood the amount of that player's bet. If the dealer stands at 21 or less, the dealer pays the bet of any player having a higher total (not exceeding 21) and collects the bet of any player having a lower total. If there is a stand-off (a player having the same total as the dealer), no chips are paid out or collected.\r\n\r\nReshuffling\r\nWhen each player's bet is settled, the dealer gathers in that player's cards and places them face up at the side against a clear plastic L-shaped shield. The dealer continues to deal from the shoe until coming to the plastic insert card, which indicates that it is time to reshuffle. Once that round of play is over, the dealer shuffles all the cards, prepares them for the cut, places the cards in the shoe, and the game continues.\r\n\r\nBasic Strategy\r\nWinning tactics in Blackjack require that the player play each hand in the optimum way, and such strategy always takes into account what the dealer's upcard is. When the dealer's upcard is a good one, a 7, 8, 9, 10-card, or ace for example, the player should not stop drawing until a total of 17 or more is reached. When the dealer's upcard is a poor one, 4, 5, or 6, the player should stop drawing as soon as he gets a total of 12 or higher. The strategy here is never to take a card if there is any chance of going bust. The desire with this poor holding is to let the dealer hit and hopefully go over 21. Finally, when the dealer's up card is a fair one, 2 or 3, the player should stop with a total of 13 or higher.\r\n\r\nWith a soft hand, the general strategy is to keep hitting until a total of at least 18 is reached. Thus, with an ace and a six (7 or 17), the player would not stop at 17, but would hit.\r\n\r\nThe basic strategy for doubling down is as follows: With a total of 11, the player should always double down. With a total of 10, he should double down unless the dealer shows a ten-card or an ace. With a total of 9, the player should double down only if the dealer's card is fair or poor (2 through 6).\r\n\r\nFor splitting, the player should always split a pair of aces or 8s; identical ten-cards should not be split, and neither should a pair of 5s, since two 5s are a total of 10, which can be used more effectively in doubling down. A pair of 4s should not be split either, as a total of 8 is a good number to draw to. Generally, 2s, 3s, or 7s can be split unless the dealer has an 8, 9, ten-card, or ace. Finally, 6s should not be split unless the dealer's card is poor (2 through 6).\r\n\r\n");
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
        Console.WriteLine("Choice is merely an illusion.");
        Console.WriteLine("However in our upcoming 49$ expansion pack, you can purchase free will");
        Console.WriteLine("Please enter your credit card details below to prepurchase for the LOW LOW price of $38.99*");
        Console.WriteLine("* not including the $69.99 prepurchase fee :)");

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
    
    public static bool Game()
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

        while (!stop && Card.Score(player1.ToArray()) <= 21) // loverly logic
        {
            Console.Clear();

            Console.WriteLine("COM:");
            OutputCardArray(new Card[] { player2.ToArray()[0] });
            Console.WriteLine("YOU:");
            OutputCardArray(player1.ToArray());
            Console.WriteLine("HIT? ");

            string playerIn = Console.ReadLine();

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

        return Card.Score(player1.ToArray()) > Card.Score(player2.ToArray());
    }

    public static void OutputCardArray(Card[] chards)
    {
        string pout = "";

        foreach (Card chard in chards)
        {
            pout += chard.GetSuiteChar() + chard.num.ToString() + " ";
        }

        Console.WriteLine(pout);
    }

    static void BJTime()
    {
        int score = 0;

        while (true)
        {
            score += Game() ? 1 : 0;

            Console.WriteLine(score);
            Console.ReadKey();
        }
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
                if (chards[i].num > 1) // not an ace
                {
                    if (chards[i].num > 10) // not a symbol card (or 10)
                    {
                        score += 10;
                    }
                    else // die nerd
                    {
                        score += chards[i].num;
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