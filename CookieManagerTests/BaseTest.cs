using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace CookieManagerTests
{
	[TestClass]
	public class BaseTest
	{
		private Random _random = null;

		public BaseTest()
		{
		}

		public IUnityContainer Container { get; private set; } = new UnityContainer();

		[TestCleanup]
		public virtual void CleanupTest()
		{
			Container.Dispose();
			Container = null;
		}

		protected Random Random
		{
			get
			{
				if (_random == null)
					_random = new Random();

				return _random;
			}
		}

		protected int Next(int maxValue, params int[] excludeValues)
		{
			int tmpValue = 0;
			bool getNextValue = true;
			while (getNextValue)
			{
				tmpValue = Random.Next(maxValue);

				if (!excludeValues.Contains(tmpValue))
					getNextValue = false;
			}

			return tmpValue;
		}

		public static void CheckForNullAndType<T>(T objectToCheck, Type type)
		{
			Assert.IsNotNull(objectToCheck, "The result was null.");
			Assert.IsInstanceOfType(objectToCheck, type, "Incorrect type was returned.");
		}
	}
}
