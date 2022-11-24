namespace Blackjack2022
{
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

