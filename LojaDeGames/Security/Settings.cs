namespace LojaDeGames.Security
{
    public class Settings
    {
        private static string secret = "7de1beac6d4b9e80098f89ebfd0acf221562023dfbd2491b62ad0f07b5920122";
        public static string Secret { get => secret; set => secret = value; }
    }
}
