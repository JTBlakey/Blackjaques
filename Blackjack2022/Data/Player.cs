using System;
using System.Text.Json;

namespace Blackjack2022
{
	public class Player
	{
		public long money { get; set; }

		public string name { get; set; }

		public Player()
		{
			this.money = 10;

			this.name = "";
		}

		public Player(long money, string name)
		{
			this.money = money;

			this.name = name;
		}

        public static void SavePlayerToFile(Player Player, string file)
        {
            string data = SavePlayer(Player);

            if (data == null)
                throw new InsufficientExecutionStackException("BOOP!");

            using (StreamWriter sw = new StreamWriter(FileLib.GetFullAddress(file)))
            {
                sw.WriteLine(data);
            }
        }

        public static string SavePlayer(Player Player)
        {
            return JsonSerializer.Serialize(Player);
        }

        public static Player LoadPlayerFromFile(string file)
        {
            string fileContents = string.Join("\n", System.IO.File.ReadAllLines(FileLib.GetFullAddress(file)));

            Player? Player = JsonSerializer.Deserialize<Player>(fileContents);

            if (Player == null)
                throw new FileLoadException("Player file loaded incorectly, please delete the file");

            return Player;
        }
    }
}

