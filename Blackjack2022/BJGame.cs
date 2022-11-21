// DO NOT TOUCH THIS FILE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack2022
{
    public class BJGame
    {
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
                bool canBurn = (player1.Count == 2) && (Card.Score(player1.ToArray()) == 13 || Card.Score(player1.ToArray()) == 14);

                Console.Clear();

                Console.WriteLine("COM:");
                OutputCardArray(player2.ToArray(), 1);
                Console.WriteLine("YOU:");
                OutputCardArray(player1.ToArray());
                Console.WriteLine();

                if (canBurn)
                {
                    Console.WriteLine("You can also press [B] to burn");
                    Console.WriteLine();
                }


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
                else if ((playerIn.ToUpper() == "B") && canBurn)
                {
                    player1.Clear();

                    player1.Add(deck[0]);
                    deck.RemoveAt(0);

                    player1.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }
            while (!stop && (Card.Score(player1.ToArray()) <= 21)); // loverly logic (much better than it was)

            Console.Clear();

            while (Card.Score(player2.ToArray()) <= 21 && (Card.Score(player1.ToArray()) >= 21 || Card.Score(player1.ToArray()) < Card.Score(player2.ToArray()))) // even better logic
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

            return new int[] { score, Card.Score(player1.ToArray()), Card.Score(player2.ToArray()) };
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

        public static void BJTime()
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

                Console.WriteLine("press enter to play again or press escape to go back to the menu");

                ConsoleKey boop = Console.ReadKey().Key;

                if (boop == ConsoleKey.Escape)
                    return;
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
}
