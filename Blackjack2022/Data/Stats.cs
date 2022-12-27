using System;
namespace Blackjack2022
{
	public class Stats
	{
		public long gamesPlayed { get; set; }

		public long moneyMade { get; set; }
		public long moneyLost { get; set; }

		public long totalCardsHit { get; set; }
		public float averageCardsHit { get { return totalCardsHit / gamesPlayed; } }

		public long totalWin { get; set; }
		public long totalLoss { get; set; }
		public float winChance { get { return totalWin / gamesPlayed; } }

		public Stats()
		{
			this.gamesPlayed = 0;

			this.moneyMade = 0;
			this.moneyLost = 0;

			this.totalCardsHit = 0;

			this.totalWin = 0;
			this.totalLoss = 0;
		}
	}
}

