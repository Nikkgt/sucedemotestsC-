namespace saucedemotests.dataprovider
{
    public static class UserProvider
    {
        private const string common_pass = "secret_sauce";
        public static (string, string) GetStandardUser(){
            return ("standard_user", common_pass);
        }

        public static (string, string) GetPerformanceGlitchUser(){
            return ("performance_glitch_user", common_pass);
        }
    }
}