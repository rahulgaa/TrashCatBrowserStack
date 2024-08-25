using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using BrowserStack;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium;

namespace alttrashcat_tests_csharp.tests
{
    public class BaseTest
    {
        public AltDriver altDriver;
        Local browserStackLocal;
        AndroidDriver<AndroidElement> appiumDriver;
        // IOSDriver<IOSElement> appiumDriver;

        [OneTimeSetUp]
        public void SetupAppium()
        {
            String BROWSERSTACK_USERNAME = Environment.GetEnvironmentVariable("mastersinteracti_Y1Vyl2");
            String BROWSERSTACK_ACCESS_KEY = Environment.GetEnvironmentVariable("ioxNqwx9szkmCuEmFA9y");
            String BROWSERSTACK_APP_ID_SDK_201 = Environment.GetEnvironmentVariable("bs://cd393c0908bea76d50d5ce9f66014876dae5db13");

            // Use dot net bindings v4.0.0 or above
            AppiumOptions capabilities = new AppiumOptions();
            Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
            browserstackOptions.Add("projectName", "TrashCat");
            browserstackOptions.Add("buildName", "TrashCat201");
            browserstackOptions.Add("sessionName", "tests - " + DateTime.Now.ToString("MMMM dd - HH:mm"));
            browserstackOptions.Add("local", "true");
            browserstackOptions.Add("idleTimeout", "300");
            browserstackOptions.Add("userName", BROWSERSTACK_USERNAME);
            browserstackOptions.Add("accessKey", BROWSERSTACK_ACCESS_KEY);
            capabilities.AddAdditionalCapability("bstack:options", browserstackOptions);
            capabilities.AddAdditionalCapability("platformName", "android");
            capabilities.AddAdditionalCapability("platformVersion", "13.0");
            capabilities.AddAdditionalCapability("appium:deviceName", "Samsung Galaxy S23 Ultra");
            // capabilities.AddAdditionalCapability("platformName", "ios");
            // capabilities.AddAdditionalCapability("platformVersion", "16");
            // capabilities.AddAdditionalCapability("appium:deviceName", "iPhone 14");
            capabilities.AddAdditionalCapability("appium:app", BROWSERSTACK_APP_ID_SDK_201);
            browserstackOptions.Add("appiumVersion", "2.6.0");

            browserStackLocal = new Local();
            List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
                        new KeyValuePair<string, string>("key", BROWSERSTACK_ACCESS_KEY)
                };
            browserStackLocal.start(bsLocalArgs);

            appiumDriver = new AndroidDriver<AndroidElement>(new Uri("https://hub-cloud.browserstack.com/wd/hub/"), capabilities);
            // appiumDriver = new IOSDriver<IOSElement>(new Uri("https://hub-cloud.browserstack.com/wd/hub/"), capabilities);

            Thread.Sleep(30000);
            Console.WriteLine("Appium driver started");
            altDriver = new AltDriver();
            Console.WriteLine("AltDriver started");

            // IWebElement ll = appiumDriver.FindElement(OpenQA.Selenium.By.Id("Allow")); //iOS
            // ll.Click(); //iOS

        }

        //browserstack has an idle timeout of max 300 seconds
        //so we need to do something with the appium driver
        //to keep it alive
        [TearDown]
        public void KeepAppiumAlive()
        {
            appiumDriver.GetDisplayDensity(); //android
            // appiumDriver.GetClipboardText(); //ios
        }

        [OneTimeTearDown]
        public void DisposeAppium()
        {
            Console.WriteLine("Ending");
            appiumDriver.Quit();
            altDriver.Stop();
            if (browserStackLocal != null)
            {
                browserStackLocal.stop();
            }
        }
    }
}
