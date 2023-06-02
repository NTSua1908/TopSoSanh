using TopSoSanh.Services.Interface;
using TopSoSanh.Entity;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using OpenQA.Selenium.Support.UI;

namespace TopSoSanh.Services.Implement
{
    public class AutoOrderService : IAutoOrderService
    {
        public bool OrderGearvn(Notification notification, string productUrl)
        {
            try
            {
                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                IWebDriver webDriver = new ChromeDriver();
                webDriver.Manage().Window.Maximize();

                webDriver.Navigate().GoToUrl(productUrl);
                webDriver.FindElement(By.Id("allowAdd2Cart")).Click();
                Thread.Sleep(1000);
                webDriver.FindElement(By.Id("checkout")).Click();
                Thread.Sleep(1000);
                webDriver.FindElement(By.Id("billing_address_full_name")).SendKeys(notification.OrderName);
                Thread.Sleep(1000);
                webDriver.FindElement(By.Id("checkout_user_email")).SendKeys(notification.OrderEmail);
                Thread.Sleep(1000);
                webDriver.FindElement(By.Id("billing_address_phone")).SendKeys(notification.PhoneNumber);
                Thread.Sleep(1000);
                webDriver.FindElement(By.Id("billing_address_phone")).SendKeys(notification.PhoneNumber);
                Thread.Sleep(1000);
                webDriver.FindElement(By.Id("billing_address_address1")).SendKeys(notification.Address);
                Thread.Sleep(1000);
                SelectElement selectProvince = new SelectElement(webDriver.FindElement(By.Id("customer_shipping_province")));
                selectProvince.SelectByText(notification.Province);
                Thread.Sleep(1000);
                SelectElement selectDistrict = new SelectElement(webDriver.FindElement(By.Id("customer_shipping_district")));
                selectDistrict.SelectByText(notification.District);
                Thread.Sleep(1000);
                SelectElement selectWard = new SelectElement(webDriver.FindElement(By.Id("customer_shipping_ward")));
                selectWard.SelectByText(notification.Commune);
                Thread.Sleep(1000);
                webDriver.FindElement(By.CssSelector("#checkout_complete > button")).Click();
                Thread.Sleep(5000);
                webDriver.Close();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
