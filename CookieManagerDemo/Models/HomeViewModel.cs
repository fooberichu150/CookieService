using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieManagerDemo.Models
{
	public class HomeViewModel
	{
		public string Name { get; set; }
		public ContrivedValues Contrived { get; set; }
	}

	public class NameRequest
	{
		public string Name { get; set; }
	}

	public class ContrivedValues
	{
		public int? Age { get; set; }
		public string Name { get; set; }
	}

}
