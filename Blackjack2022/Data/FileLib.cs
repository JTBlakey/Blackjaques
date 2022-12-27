namespace Blackjack2022
{
    internal class FileLib
    {
        public const string OLD_SETTINGS_FILE_LOCATION = "/SETTINGS/settings.json"; // old depricated location - will be removed in future update

        public const string RULES_FILE_LOCATION = "/Ass/Rules.txt";
        public const string CREDITS_FILE_LOCATION = "/Ass/Credits.txt";
        public const string SETTINGS_FILE_LOCATION = "/DATA/settings.json";
        public const string PLAYER_FILE_LOCATION = "/DATA/player.json";


        public static string GetFullAddress(string file)
        {
            return AppDomain.CurrentDomain.BaseDirectory + file;
        }
    }
}
