﻿using System.Text.Json;

namespace Blackjack2022
{
    public class FileLib
    {
        public const string OLD_SETTINGS_FILE_LOCATION = "SETTINGS/settings.json"; // old depricated location - will be removed in future update

        public const string RULES_FILE_LOCATION = "Ass/Rules.txt";
        public const string CREDITS_FILE_LOCATION = "Ass/Credits.txt";
        public const string SETTINGS_FILE_LOCATION = "DATA/settings.json";
        public const string PLAYER_FILE_LOCATION = "DATA/player.json";
        public const string STATS_FILE_LOCATION = "DATA/stats.json";

        public static string GetFullAddress(string file)
        {
            return AppDomain.CurrentDomain.BaseDirectory + file;
        }

        public static T? Open<T>(string address)
        {
            address = GetFullAddress(address);

            T? data = default(T);

            try
            {
                string fileContents = string.Join("\n", System.IO.File.ReadAllLines(address));

                data = JsonSerializer.Deserialize<T>(fileContents);
            }
            catch { }

            return data;
        }

        public static void Save<T>(T data, string address)
        {
            address = GetFullAddress(address);

            string fileContents = JsonSerializer.Serialize<T>(data);

            using (StreamWriter sw = new StreamWriter(address))
            {
                sw.WriteLine(fileContents);
            }
        }
    }
}
