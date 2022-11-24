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
			int width = 0;

			foreach (string menuItem in menuItems)
			{
				if (menuItem.Length > width)
					width = menuItem.Length;
			}

			for (int i = 1; i <= menuItems.Length; i++)
			{
				int expanse = (width - menuItems[i - 1].Length) + 1;

				Console.WriteLine("[" + i.ToString() + ":  " + menuItems[i - 1] + String.Concat(Enumerable.Repeat(" ", expanse)) + "]");
			}

			Console.WriteLine();
			Console.Write((inputPrompt == "") ? "Option: " : inputPrompt);

			while (true)
			{
				ConsoleKey input = Console.ReadKey().Key;

				if (input == menuReturn)
					return -1;

				for (int i = 1; i <= menuItems.Length; i++)
				{
					if (input == inputKeys[i - 1])
						return i;
				}
			}
		}
	}
}

