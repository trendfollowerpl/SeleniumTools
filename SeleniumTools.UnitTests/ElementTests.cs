using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumTools.Element;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTools;

namespace SeleniumTools.UnitTests
{
	[Category("SeleniumTools Element")]
	[TestFixture]
	public class ElementTests
	{
		[TestCaseSource(typeof(ElementTestData), nameof(ElementTestData.Data))]
		[Description("Check if LocatorStrategy is properly initialized")]
		public void Element01_CreateNewStrategyElement(Tuple<string, Func<string, By>> testData)
		{
			var locator = testData.Item1;
			var byFunc = testData.Item2;
			var strategy = new LocatorStrategy(byFunc, locator);

			Assert.That(strategy.ByStrategy, Is.InstanceOf(typeof(By)));
			Assert.That(strategy.ByStrategy.ToString(), Is.EqualTo(byFunc.Invoke(locator).ToString()));
			Assert.That(strategy.Locator, Is.EqualTo(locator));
		}
		[Test]
		public void Element02_UpdateNewStrategyElement()
		{
			var strategy1 = new LocatorStrategy(By.Id, "test");
			var strategy2 = strategy1.UpdateStrategy(By.ClassName, "class");

			Assert.That(strategy2.ByStrategy.ToString(), Is.EqualTo(By.ClassName("class").ToString()));
			Assert.That(strategy2.Locator, Is.EqualTo("class"));
		}
	}


	class ElementTestData
	{
		public static IEnumerable<Tuple<string, Func<string, By>>> Data
		{
			get
			{
				yield return new Tuple<string, Func<string, By>>(nameof(By.CssSelector), By.CssSelector);
				yield return new Tuple<string, Func<string, By>>(nameof(By.Id), By.Id);
				yield return new Tuple<string, Func<string, By>>(nameof(By.XPath), By.XPath);
			}
		}
	}
}
