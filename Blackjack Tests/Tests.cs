using Blackjack2022;

namespace Blackjack_Tests;

[TestClass]
public class Tests
{
    [TestMethod]
    public void TestSettings()
    {
        Settings[] settings = new Settings[16 * 16];

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                settings[(x * 16) + y] = new Settings(((x * 16) + y).ToString(), (ConsoleColor)x, (ConsoleColor)y);
            }
        }

        string loc = "./TMPTEST";

        if (!Directory.Exists(loc))
        {
            Directory.CreateDirectory(loc);
        }

        foreach (Settings setting in settings)
        {
            Settings.SaveSettingsToFile(setting, Path.Join(loc, (setting.name + ".json")));
        }

        foreach (Settings setting in settings)
        {
            Settings settingLoad = Settings.LoadSettingsFromFile(Path.Join(loc, (setting.name + ".json")));

            if (settingLoad.foregroundColor != setting.foregroundColor)
                throw new Exception("Setting files not equall");

            if (settingLoad.backgroundColor != setting.backgroundColor)
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

    [TestMethod]
    public void TestWinnerSystem()
    {
        List<KeyValuePair<int, int[]>> winnerVals = new List<KeyValuePair<int, int[]>>();

        winnerVals.Add(new KeyValuePair<int, int[]>(0, new int[] { 21, 21 }));
        winnerVals.Add(new KeyValuePair<int, int[]>(1, new int[] { 8, 67 }));
        winnerVals.Add(new KeyValuePair<int, int[]>(2, new int[] { 38, 17 }));
        winnerVals.Add(new KeyValuePair<int, int[]>(0, new int[] { 29, 90 }));

        foreach (KeyValuePair<int, int[]> winnerVal in winnerVals)
        {
            if (BJGame.GetWinner(winnerVal.Value[0], winnerVal.Value[1]) != winnerVal.Key)
                throw new FormatException("Winner given is incorrect");
        }
    }
}
