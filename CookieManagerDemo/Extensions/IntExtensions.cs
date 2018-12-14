using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieManagerDemo.Extensions
{
	public static class IntExtensions
	{
		public static bool IsInRange(this int checkVal, int value1, int value2)
		{
			// First check to see if the passed in values are in order. If so, then check to see if checkVal is between them
			if (value1 <= value2)
				return checkVal >= value1 && checkVal <= value2;

			// Otherwise invert them and check the checkVal to see if it is between them
			return checkVal >= value2 && checkVal <= value1;
		}
	}
}
