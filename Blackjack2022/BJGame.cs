// DO NOT TOUCH THIS FILE

namespace Blackjack2022
{
    public class BJGame
    {
        public class GameReturnData
        {
            public int p1Score;
            public int p2Score;

            public string? p1WinCondition;
            public string? p2WinCondition;

            public bool p1Won;

            public GameReturnData() { }

            public GameReturnData(int p1Score, int p2Score)
            {
                this.p1Score = p1Score;
                this.p2Score = p2Score;

                this.p1WinCondition = null;
                this.p2WinCondition = null;

                p1Won = (GetWinner(p1Score, p2Score) != 2);
            }

            public GameReturnData(int p1Score, int p2Score, string? p1WinCondition, string? p2WinCondition)
            {
                this.p1Score = p1Score;
                this.p2Score = p2Score;

                this.p1WinCondition = p1WinCondition;
                this.p2WinCondition = p2WinCondition;

                p1Won = (GetWinner(p1Score, p2Score) != 2);
            }

            public string P1ScoreString()
            {
                if (p1WinCondition == null)
                    return p1Score.ToString();

                return p1WinCondition;
            }

            public string P2ScoreString()
            {
                if (p2WinCondition == null)
                    return p2Score.ToString();

                return p2WinCondition;
            }
        }

        public static GameReturnData Game()
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

                Console.WriteLine("COM:" + ((Program.debugPlusPlus) ? Card.Score(player2.ToArray()).ToString() : ""));
                OutputCardArray(player2.ToArray(), (uint)((Program.debugPlusPlus) ? 2 : 1));
                Console.WriteLine("YOU:" + ((Program.debugPlusPlus) ? Card.Score(player1.ToArray()).ToString() : ""));
                OutputCardArray(player1.ToArray());
                Console.WriteLine();

                if (canBurn)
                {
                    Console.WriteLine("You can also press [B] to burn");
                    Console.WriteLine();
                }

                #if DEBUG
                if (Program.debugPlusPlus)
                    OutputCardArray(deck.ToArray());
                #endif


                Console.Write("HIT? ");

                string? playerIn = Console.ReadLine();
                Console.WriteLine();

                if (playerIn != null)
                {
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
            }
            while ((!stop && (Card.Score(player1.ToArray()) <= 21)) && player1.Count < 5); // loverly logic (much better than it was)

            Console.Clear();

            // player1 <= 21, player2 <= player1

            if (Card.Score(player1.ToArray()) > 0)
            {
                while (Card.Score(player1.ToArray()) <= 21 && (Card.Score(player2.ToArray(), true) <= Card.Score(player1.ToArray()))) // even better logic
                {
                    player2.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }

            Console.WriteLine("COM:");
            OutputCardArray(player2.ToArray());
            Console.WriteLine("YOU:");
            OutputCardArray(player1.ToArray());

            if (Card.Score(player1.ToArray()) > 0)
                return new GameReturnData(Card.Score(player1.ToArray()), Card.Score(player2.ToArray(), true));
            else
                return new GameReturnData(Card.Score(player1.ToArray()), Card.Score(player2.ToArray(), true), "5 card rule", null);
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
                GameReturnData gs = Game();
                score += gs.p1Won ? 1 : 0;

                Console.WriteLine();
                Console.WriteLine(gs.p1Won ? "you won!" : "you lost :(");
                Console.WriteLine();
                Console.WriteLine("the dealers cards where worth: " + gs.P2ScoreString());
                Console.WriteLine("your cards where worth: " + gs.P1ScoreString());
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
            if (score1 < 0)
                return 1;

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
    }
}
