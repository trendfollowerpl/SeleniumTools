using System;

namespace SeleniumTools.Extentions
{
	public static class FunctionaExt
	{
		public static TResult Using<TDisposable, TResult>(
			Func<TDisposable> disposableFactory,
			Func<TDisposable, TResult> map)
			where TDisposable : IDisposable
		{
			using (var disposable = disposableFactory())
			{
				return map(disposable);
			}
		}
	}
}
