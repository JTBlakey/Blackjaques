using System;

namespace Blackjack2022
{
	public class Menu
	{
		private static ConsoleKey[] inputKeys =
		{
			ConsoleKey.D1,
			ConsoleKey.D2,
			ConsoleKey.D3,
			ConsoleKey.D4,
			ConsoleKey.D5,
			ConsoleKey.D6,
			ConsoleKey.D7,
			ConsoleKey.D8,
			ConsoleKey.D9,
		};

		public static int NumberMenu(string[] menuItems, string inputPrompt = "", ConsoleKey? menuReturn = null)
		{
			for (int i = 1; i <= menuItems.Length; i++)
			{
				Console.WriteLine("[" + i.ToString() + " " + menuItems[i - 1] + "]");
			}

			Console.WriteLine();
			Console.WriteLine(inputPrompt);

			while (true)
			{
				ConsoleKey input = Console.ReadKey().Key;

				for (int i = 1; i <= menuItems.Length; i++)
				{
					if (input == inputKeys[i])
						return i;
				}
			}
		}
	}
}

