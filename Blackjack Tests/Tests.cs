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

            if (settingLoad.name != setting.name)
                throw new Exception("Setting files not equall");
        }

        Directory.Delete(loc, true);
    }
}
