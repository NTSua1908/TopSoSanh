using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TopSoSanh.Entity;
using TopSoSanh.Services.Interface;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace TopSoSanh.Services.Implement
{
	public class AutoOrderService : IAutoOrderService
	{
		public bool OrderGearvn(Notification notification, string productUrl)
		{
			new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
			IWebDriver webDriver = new ChromeDriver();
			try
			{
				webDriver.Manage().Window.Maximize();
				webDriver.Navigate().GoToUrl(productUrl);
				webDriver.FindElement(By.Id("buy-now")).Click();
				Thread.Sleep(1000);
				webDriver.Navigate().GoToUrl("https://gearvn.com/cart#cart-buy-order-box");
				Thread.Sleep(1000);
				webDriver.FindElement(By.CssSelector(".btn-checkout")).Click();
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("editcustomer-name")).SendKeys(notification.OrderName);
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("editcustomer-phone")).SendKeys(notification.PhoneNumber);
				Thread.Sleep(1000);
				string province = notification.Province;
				if (province.ToLower() == "tp hcm")
				{
					province = "Hồ Chí Minh";
				}
				SelectElement selectProvince = new SelectElement(webDriver.FindElement(By.CssSelector("select.select-province")));
				selectProvince.SelectByText(notification.Province);
				Thread.Sleep(1000);
				SelectElement selectDistrict = new SelectElement(webDriver.FindElement(By.CssSelector("select.select-district")));
				selectDistrict.SelectByText(notification.District);
				Thread.Sleep(1000);
				SelectElement selectWard = new SelectElement(webDriver.FindElement(By.CssSelector("select.select-ward")));
				selectWard.SelectByText(notification.Commune);
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("editcustomer-address")).SendKeys(notification.Address);
				Thread.Sleep(1500);
				IWebElement orderBtn = webDriver.FindElement(By.CssSelector("div.summary-action > a"));
				//WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
				//wait.Until(ExpectedConditi.ElementToBeClickable(element));
				//// Now you can interact with the element
				//element.Click();
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("checkout")).Click();
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				Thread.Sleep(5000);
				webDriver.Close();
			}

			return true;
		}

		public bool OrderAnPhat(Notification notification, string productUrl)
		{
			new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
			IWebDriver webDriver = new ChromeDriver();
			try
			{
				webDriver.Manage().Window.Maximize();
				webDriver.Navigate().GoToUrl(productUrl);
				webDriver.FindElement(By.CssSelector("a.w-100.btn-buyNow.js-buy-now")).Click();
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("buyer_name")).SendKeys(notification.OrderName);
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("buyer_email")).SendKeys(notification.OrderEmail);
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("buyer_tel")).SendKeys(notification.PhoneNumber);
				Thread.Sleep(1000);
				SelectElement selectProvince = new SelectElement(webDriver.FindElement(By.Id("ship_to_province")));
				selectProvince.SelectByText(notification.Province);
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("buyer_address")).SendKeys(notification.District + ", " + notification.Commune + ", " + notification.Address);
				Thread.Sleep(1000);
				webDriver.FindElement(By.CssSelector("#btn-submit")).Click();
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				Thread.Sleep(5000);
				webDriver.Close();
			}
			return true;
		}

		public bool OrderAnKhang(Notification notification, string productUrl)
		{
			new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
			IWebDriver webDriver = new ChromeDriver();
			try
			{
				webDriver.Manage().Window.Maximize();
				webDriver.Navigate().GoToUrl(productUrl);
				webDriver.FindElement(By.CssSelector("div.add_now_cart.clearfix > a.now_cart")).Click();
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("js-check-agree")).Click();
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("buyer_name")).SendKeys(notification.OrderName);
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("buyer_tel")).SendKeys(notification.PhoneNumber);
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("buyer_email")).SendKeys(notification.OrderEmail);
				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("buyer_address")).SendKeys(notification.Province + ", " + notification.District + ", " + notification.Commune + ", " + notification.Address);

				Thread.Sleep(1000);
				var chat = webDriver.FindElement(By.CssSelector(".widget-visible"));
				IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
				jsExecutor.ExecuteScript("arguments[0].remove();", chat);

				Thread.Sleep(1000);
				webDriver.FindElement(By.Id("pay_method2")).Click();
				webDriver.FindElement(By.CssSelector(".btn-buy-order > input.submit")).Click();
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				Thread.Sleep(5000);
				webDriver.Close();
			}
			return true;
		}

	}
}
