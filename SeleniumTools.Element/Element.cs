using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTools.Element
{
	public sealed class SelElement
	{
		private IWebDriver _driver { get; }
		private IList<LocatorStrategy> _strategies { get; }
		private SelElement _parent { get; }

		public SelElement(IWebDriver driver, IList<LocatorStrategy> strategies, SelElement parent = null)
		{
			_driver = driver;
			_strategies = strategies;
			_parent = parent;
		}

		public Task<IWebElement> LocateElement()
		{
			IList<Task<IWebElement>> locationTasks = new List<Task<IWebElement>>();

			_strategies.ToList()
				.ForEach(strategy => locationTasks.Add(Task.Factory.StartNew(() =>
					{
						try
						{
							Debug.WriteLine($"trying to locate by : {strategy.ByStrategy}");
							return _driver.FindElement(strategy.ByStrategy);
						}
						catch (NoSuchElementException)
						{
							Debug.WriteLine($"Element is not located by : {strategy.ByStrategy}");
							return null;
						}
					})));

			return
				Task.WhenAll(locationTasks.ToArray())
					.ContinueWith(tasks => tasks.Result.FirstOrDefault(t => t != null));
		}
	}
}
