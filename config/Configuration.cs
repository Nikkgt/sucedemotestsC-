using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using saucedemotests.utils;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace saucedemotests.config
{
    public class Configuration
    {
        private static readonly Configuration config = new Configuration();
        private readonly Dictionary<string, string> properties = new Dictionary<string, string>();
        private const string configFilePath = "config/config.properties";

        // Global variables
        private const string browserID = "browserID";
        private const string baseURL = "baseURL";
        private const string headless = "headless";


        private Configuration()
        {
            ReadProperties();
        }

        public static Configuration GetInstance()
        {
            return config;
        }

        public string GetBrowserID()
        {
            return GetPropertyValue(browserID);
        }

        private void SetOptions(DriverOptions options)
        {
            if (bool.Parse(GetPropertyValue(headless)))
            {
                if (options is ChromeOptions chromeOptions)
                {
                    chromeOptions.AddArgument(headless);
                }
                else if (options is FirefoxOptions firefoxOptions)
                {
                    firefoxOptions.AddArgument(headless);
                }
                else if (options is EdgeOptions edgeOptions)
                {
                    edgeOptions.AddArgument(headless);
                }
            }
        }
        public WebDriver GetWebDriver(string browserID)
        {
            int browserId = NumberUtil.GetIntFromStringWithDefault(browserID, 1);

            var coptions = new ChromeOptions();
            SetOptions(coptions);

            switch (browserId)
            {
                case (int)BrowserType.Chrome:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver(coptions);

                case (int)BrowserType.Firefox:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    var foptions = new FirefoxOptions();
                    SetOptions(foptions);
                    return new FirefoxDriver(foptions);

                case (int)BrowserType.Edge:
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    var eoptions = new EdgeOptions();
                    SetOptions(eoptions);
                    return new EdgeDriver(eoptions);

                default:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver(coptions);

            }
        }

        public string GetBaseURL()
        {
            return GetPropertyValue(baseURL);
        }

        private void ReadProperties()
        {
            try
            {
                foreach (var line in File.ReadAllLines(configFilePath))
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                        continue;

                    string[] pair = line.Split('=', 2);
                    if (pair.Length == 2)
                    {
                        properties[pair[0].Trim()] = pair[1].Trim();
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("An error occured in reading property file");
            }

        }

        private string GetPropertyValue(string key)
        {
            if (properties.TryGetValue(key, out var value))
            {
                return value;
            }
            throw new KeyNotFoundException($"Property '{key}' not found in configuration file.");
        }
    }
}