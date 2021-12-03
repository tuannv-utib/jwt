using JwtAuthentication.Helpers;

namespace JwtAuthentication.Configure
{
    public class AppKeys
    {
        public static string JWTSecretKey = AppSettings.GetAppKey("JWTSecretKey");
        public static int JWTTimeout = AppSettings.GetAppKey<int>("JWTTimeout");
    }
}
