namespace BookCollection.Api.Configuration
{
    public static class GlobalConfig
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetSetting(string key)
        {
            return _configuration[key];
        }
    }
}
