using System;

public static class EventExtensions
{
	public static void Fire(this Action eventAction)
	{
		if (eventAction != null)
		{
			eventAction.Invoke();
		}
	}

	public static void Fire<T>(this Action<T> eventAction, T firstArg)
	{
		if (eventAction != null)
		{
			eventAction.Invoke(firstArg);
		}
	}

	public static void Fire<T, U>(this Action<T, U> eventAction, T firstArg, U secondArg)
	{
		if (eventAction != null)
		{
			eventAction.Invoke(firstArg, secondArg);
		}
	}

	public static void Fire<T, U, V>(this Action<T, U, V> eventAction, T firstArg, U secondArg, V thirdArg)
	{
		if (eventAction != null)
		{
			eventAction.Invoke(firstArg, secondArg, thirdArg);
		}
	}
}