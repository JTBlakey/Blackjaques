using System.Text.Json;

namespace Blackjack2022
{
    public class Settings
    {
        public ConsoleColor foregroundColor { get; set; }
        public ConsoleColor backgroundColor { get; set; }

        public string name { get; set; }

        public Settings()
        {
            name = "Username";

            foregroundColor = ConsoleColor.White;
            backgroundColor = ConsoleColor.Black;
        }

        public Settings(string sName, ConsoleColor foreground, ConsoleColor background)
        {
            name = sName;

            try
            {
                foregroundColor = foreground;
                backgroundColor = background;
            }
            catch
            {
                foregroundColor = ConsoleColor.White;
                backgroundColor = ConsoleColor.Black;
            }            
        }

        public void SwapColors()
        {
            ConsoleColor temp = foregroundColor;

            foregroundColor = backgroundColor;
            backgroundColor = temp;
        }

        public static void SaveSettingsToFile(Settings settings, string file)
        {
            string data = SaveSettings(settings);

            if (data == null)
                throw new InsufficientExecutionStackException("BOOP!");

            using (StreamWriter sw = new StreamWriter(FileLib.GetFullAddress(file)))
            {
                sw.WriteLine(data);
            }
        }

        public static string SaveSettings(Settings settings)
        {
            return JsonSerializer.Serialize(settings);
        }

        public static Settings LoadSettingsFromFile(string file)
        {
            string fileContents = string.Join("\n", System.IO.File.ReadAllLines(FileLib.GetFullAddress(file)));

            Settings? settings = JsonSerializer.Deserialize<Settings>(fileContents);

            if (settings == null)
                throw new FileLoadException("Settings file loaded incorectly, please delete the file");

            LoadSettings(settings);

            return settings;
        }

        public static void LoadSettings(Settings settings)
        {
            Console.BackgroundColor = settings.backgroundColor;
            Console.ForegroundColor = settings.foregroundColor;
        }
    }
}
