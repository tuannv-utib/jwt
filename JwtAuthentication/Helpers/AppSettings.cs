using Microsoft.Extensions.Configuration;

namespace JwtAuthentication.Helpers
{
    public class AppSettings
    {
        public const string AppKeyFormat = "AppKeys:";
        private static AppSettings _instance;
        private static readonly object ObjLocked = new object();
        private IConfiguration _configuration;

        protected AppSettings()
        {
        }

        public void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static AppSettings Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (ObjLocked)
                    {
                        if (null == _instance)
                            _instance = new AppSettings();
                    }
                }
                return _instance;
            }
        }

        private static T GetObject<T>(string key = null)
        {
            var section = Instance._configuration.GetSection(key);
            return section.Get<T>();
        }

        private static string GetObject(string key = null)
        {
            var section = Instance._configuration.GetSection(key);
            return section.Value;
        }

        public static T GetAppKey<T>(string key = null)
        {
            var fullKey = AppKeyFormat + key;
            try
            {
                return GetObject<T>(fullKey);
            }
            catch
            {
                return default(T);
            }
        }
        public static string GetAppKey(string key = null)
        {
            var fullKey = AppKeyFormat + key;
            try
            {
                return GetObject(fullKey);
            }
            catch
            {
                return null;
            }
        }
    }
}
