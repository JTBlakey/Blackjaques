using System;
using System.Text.Json;

namespace Blackjack2022
{
	public class Stats
	{
		public long gamesPlayed { get; set; }

		public long moneyMade { get; set; }
		public long moneyLost { get; set; }

		public long totalCardsHit { get; set; }
		public float averageCardsHit { get { return Divide(totalCardsHit, gamesPlayed); } }

		public long totalWin { get; set; }
		public long totalLoss { get; set; }
		public float winChance { get { return Divide(totalWin, gamesPlayed); } }

		public Stats()
		{
			this.gamesPlayed = 0;

			this.moneyMade = 0;
			this.moneyLost = 0;

			this.totalCardsHit = 0;

			this.totalWin = 0;
			this.totalLoss = 0;
		}

        private static float Divide(float a, float b, uint percision = 2)
        {
            if (a == 0 || b == 0)
                return 0;

            uint power = (uint)MathF.Pow(10, percision);

            return ((float)((long)((a / b) * power))) / power;
        }

        public static void SaveStatsToFile(Stats Stats, string file)
        {
            string data = SaveStats(Stats);

            if (data == null)
                throw new InsufficientExecutionStackException("BOOP!");

            using (StreamWriter sw = new StreamWriter(FileLib.GetFullAddress(file)))
            {
                sw.WriteLine(data);
            }
        }

        public static string SaveStats(Stats Stats)
        {
            return JsonSerializer.Serialize(Stats);
        }

        public static Stats LoadStatsFromFile(string file)
        {
            string fileContents = string.Join("\n", System.IO.File.ReadAllLines(FileLib.GetFullAddress(file)));

            Stats? Stats = JsonSerializer.Deserialize<Stats>(fileContents);

            if (Stats == null)
                throw new FileLoadException("Stats file loaded incorectly, please delete the file");

            return Stats;
        }
    }
}

