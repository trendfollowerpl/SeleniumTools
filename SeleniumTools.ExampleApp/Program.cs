using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using SeleniumTools.Element;
using SeleniumTools.Extentions;

namespace SeleniumTools.ExampleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var driver = new ChromeDriver(@"c:\temp");

			var element = new SelElement(driver, new List<LocatorStrategy>
			{
				new LocatorStrategy(By.CssSelector, "#header>h1>aa") ,
				new LocatorStrategy(By.XPath, "//*[@id='mainContent']/h2[3]")
			});

			driver.Navigate().GoToUrl("http://www.seleniumhq.org/");

			element.LocateElement()
				.ContinueWith(t =>
				{
					var result = t.Result;
					Console.WriteLine($"{result.Text}");
				}).Wait();

			driver.Close();

			Console.ReadLine();
		}
	}
}
