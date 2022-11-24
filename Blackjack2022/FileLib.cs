namespace Blackjack2022
{
    internal class FileLib
    {
        public const string SETTINGS_FILE_LOCATION = "/SETTINGS/settings.json";
        public const string RULES_FILE_LOCATION = "/Ass/Rules.txt";
        public const string CREDITS_FILE_LOCATION = "/Ass/Credits.txt";

        public static string GetFullAddress(string file)
        {
            return AppDomain.CurrentDomain.BaseDirectory + file;
        }
    }
}
