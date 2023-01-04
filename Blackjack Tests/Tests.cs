using Blackjack2022;

namespace Blackjack_Tests;

[TestClass]
public class Tests
{
    [TestMethod]
    public void TestSettings()
    {
        Settings[] settings = new Settings[16 * 16];
        string[] names = new string[16 * 16];

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                settings[(x * 16) + y] = new Settings((ConsoleColor)x, (ConsoleColor)y);
                names[(x * 16) + y] = ((x * 16) + y).ToString();
            }
        }

        string loc = "./TMPTEST";

        if (!Directory.Exists(loc))
        {
            Directory.CreateDirectory(loc);
        }

        for (int i = 0; i < 16 * 16; i++)
        {
            FileLib.Save<Settings>(settings[i], Path.Join(loc, (names[i] + ".json")));
        }

        for (int i = 0; i < 16 * 16; i++)
        {
            Settings settingLoad = FileLib.Open<Settings>(Path.Join(loc, (names[i] + ".json")));

            if (settingLoad.foregroundColor != settings[i].foregroundColor)
                throw new Exception("Setting files not equall");

            if (settingLoad.backgroundColor != settings[i].backgroundColor)
                throw new Exception("Setting files not equall");
        }

        Directory.Delete(loc, true);
    }

    [TestMethod]
    public void TestDealDeck()
    {
        Card[] deck = Card.Deck();

        if (deck.Length != 52)
            throw new FormatException("Deck should be 52 cards long");

        List<Card> deckLog = new List<Card>();

        foreach (Card card in deck)
        {
            if (deckLog.Contains(card))
                throw new FormatException("Deck should not contain duplicate cards");

            deckLog.Add(card);
        }
    }

    [TestMethod]
    public void TestHandScoreSystem()
    {
        Dictionary<int, Card[]> handScores = new Dictionary<int, Card[]>();

        handScores.Add(21, new Card[] { new Card(0, 0), new Card(0, 10) });
        handScores.Add(2, new Card[] { new Card(0, 0), new Card(1, 0) });
        handScores.Add(22, new Card[] { new Card(0, 10), new Card(1, 10), new Card(0, 1) });

        foreach (KeyValuePair<int, Card[]> handScore in handScores)
        {
            if (handScore.Key != Card.Score(handScore.Value))
                throw new FormatException("Cards scored incorrectly");
        }
    }
}
