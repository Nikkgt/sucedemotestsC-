using System.Text.RegularExpressions;
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

        private Configuration()
        {
            ReadProperties();
        }

        public static Configuration GetInstance()
        {
            return config;
        }

        public string GetBrowserID(){
            return GetPropertyValue("browserID");
        }

        public WebDriver GetWebDriver(string browserID)
        {
            int browserId = NumberUtil.GetIntFromStringWithDefault(browserID, 1);

            switch (browserId)
            {
                case (int)BrowserType.Chrome:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();

                case (int)BrowserType.Firefox:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();

                case (int)BrowserType.Edge:
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();

                default:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();

            }
        }

        public string GetBaseURL()
        {
            return GetPropertyValue("baseURL");
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