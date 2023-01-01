// DO NOT TOUCH THIS FILE

namespace Blackjack2022
{
    public class BJGame
    {
        public class GameReturnData
        {
            public int dealerScore;

            public int[] playerScores;

            public string? dealerWinCondition;
            public string?[] playerWinConditions;

            public bool dealerWon;

            public bool[] playerWon;

            public GameReturnData() { }

            public GameReturnData(int dealerScore, int[] playerScores)
            {
                this.dealerScore = dealerScore;
                this.playerScores = playerScores;

                this.dealerWinCondition = null;

                this.playerWinConditions = new string?[playerScores.Length];

                bool[] win = GetWinner(dealerScore, playerScores);
                dealerWon = win[0];
                playerWon = new bool[playerScores.Length];

                for (int i = 1; i < win.Length; i++)
                    playerWon[i - 1] = win[i];
            }

            public GameReturnData(int p1Score, int p2Score, string? p1WinCondition, string? p2WinCondition)
            {
                this.p1Score = p1Score;
                this.p2Score = p2Score;

                this.p1WinCondition = p1WinCondition;
                this.p2WinCondition = p2WinCondition;

                p1Won = (GetWinner(p1Score, p2Score) != 2);
            }

            public string DealerScoreString()
            {
                if (this.dealerWinCondition == null)
                    return this.dealerScore.ToString();

                return this.dealerWinCondition;
            }

            public string PlayerWinString(int player)
            {
                if (this.playerWinConditions[player] == null)
                    return this.playerScores[player].ToString();

#pragma warning disable CS8603 // Possible null reference return. -- imposible - checked above (unless ram is fucked with)
                return this.playerWinConditions[player];
#pragma warning restore CS8603 // Possible null reference return.
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

                        Program.stats.totalCardsHit++;
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

            // player1 <= 21 && player2 <= player1

            if (Card.Score(player1.ToArray()) > 0)
            {
                while (Card.Score(player1.ToArray()) <= 21 && (Card.Score(player2.ToArray(), true) <= Card.Score(player1.ToArray()))) // even better logic
                {
                    player2.Add(deck[0]);
                    deck.RemoveAt(0);

#if DEBUG
                    if (Program.debugPlusPlus)
                    {
                        Console.Clear();

                        Console.WriteLine("dealer dealing cards to dealer");
                        OutputCardArray(player2.ToArray());
                        Console.WriteLine(Card.Score(player2.ToArray()).ToString());
                        OutputCardArray(deck.ToArray());

                        Console.ReadLine();
                    }
#endif
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

        public static long BJTime(long money = 10)
        {
            int score = 0;

            while (true)
            {
                long bet = -1;

                while (true)
                {
                    Console.Clear();

                    Console.WriteLine("Balance: $" + money.ToString());
                    Console.Write("Bet: $");

                    string? betInputString = Console.ReadLine();

                    try
                    {
                        bet = Convert.ToInt64(betInputString);

                        if (bet >= 0 && bet <= money)
                        {
                            break;
                        }
                    }
                    catch { }
                }

                GameReturnData gs = Game();
                score += gs.p1Won ? 1 : 0;

                if (money == 0 && gs.p1Won)
                {
                    Program.stats.moneyMade++;

                    money++;
                }

                money += (gs.p1Won) ? bet : -bet;

                if (gs.p1Won)
                {
                    Program.stats.moneyMade += bet;
                    Program.stats.totalWin++;
                }
                else
                {
                    Program.stats.moneyLost += bet;
                    Program.stats.totalLoss++;
                }

                Program.stats.gamesPlayed++;

                Console.Clear();

                Console.WriteLine();
                Console.WriteLine((!gs.dealerWon) ? "you won!" : "you lost :(");
                Console.WriteLine();
                Console.WriteLine("the dealers cards where worth: " + gs.P2ScoreString());
                Console.WriteLine("your cards where worth: " + gs.P1ScoreString());
                Console.WriteLine();
                Console.WriteLine("Score: " + score.ToString());
                Console.WriteLine("Balance: $" + money.ToString());

                Console.WriteLine("\npress enter to play again or press escape to go back to the menu");

                ConsoleKey boop = Console.ReadKey().Key;

                if (boop == ConsoleKey.Escape)
                    return money;
            }
        }

        public static bool[] GetWinner(int dealerScore, int[] playerScores) // 0 - dealer, 1, ∞ - playerScores + 1
        {
            bool[] scores = new bool[playerScores.Length + 1];

            bool overide = false;

            for (int i = 0; i < playerScores.Length; i++) // check for negavtive scores - OVERRIDE
            {
                if (playerScores[i] < 0)
                {
                    scores[i + 1] = true;

                    overide = true;
                }
            }

            if (overide)
                return scores;

            bool dealerHighest = true;

            for (int i = 0; i < playerScores.Length; i++)
            {
                if (dealerScore <= playerScores[i])
                    dealerHighest = false;
            }

            if (dealerHighest)
            {
                scores[0] = true;

                return scores;
            }

            int highestScore = 0;

            for (int i = 0; i < playerScores.Length; i++)
            {
                if (playerScores[i] > highestScore)
                    highestScore = playerScores[i];
            }

            for (int i = 0; i < playerScores.Length; i++)
            {
                if (playerScores[i] == highestScore)
                    scores[i] = true;
            }

            return scores;
        }
    }
}
