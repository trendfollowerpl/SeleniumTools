using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTools.Element
{
	public sealed class LocatorStrategy
	{
		public LocatorStrategy(Func<string, By> byFync, string locator)
		{
			Locator = locator;
			ByStrategy = byFync(locator);
		}
		public By ByStrategy { get; private set; }
		public string Locator { get; private set; }

		public LocatorStrategy UpdateStrategy(Func<string, By> byFync, string locator) => new LocatorStrategy(byFync, locator);
	}
}
