using System;
using CookieManagerDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Unity;

namespace CookieManagerTests
{
	[TestClass]
	public class CookieServiceTests : BaseTest
	{
		IHttpContextAccessor _httpContextAccessor;
		HttpContext _httpContext;
		CookieService _target;

		[TestInitialize]
		public void Initialize()
		{
			_httpContextAccessor = Substitute.For<IHttpContextAccessor>();

			_httpContext = new DefaultHttpContext();
			_httpContextAccessor.HttpContext.Returns(_httpContext);

			Container.RegisterInstance(_httpContextAccessor);

			_target = Container.Resolve<CookieService>();
		}

		[TestMethod]
		public void CookieService_SetCookie_Success()
		{
			CookieFake cookie = new CookieFake { TestProperty = 25, TestPropertyString = "blah" };
			_target.Set("fakecookie", cookie);

			CookieFake cachedCookie = _target.Get<CookieFake>("fakecookie");

			Assert.IsNotNull(cachedCookie);
			Assert.AreEqual(cookie.TestProperty, cachedCookie.TestProperty);
			Assert.AreEqual(cookie.TestPropertyString, cachedCookie.TestPropertyString);
		}

		[TestMethod]
		public void CookieService_SetCookie_StringOnly_Success()
		{
			string value = "I'm a cookie value";
			_target.Set("fakecookie", value);

			string result = _target.Get<string>("fakecookie");

			Assert.IsFalse(string.IsNullOrWhiteSpace(result));
			Assert.AreEqual(value, result);
		}

		[TestMethod]
		public void CookieService_SetCookie_Base64_Success()
		{
			CookieFake cookie = new CookieFake { TestProperty = 25, TestPropertyString = "blah" };
			_target.Set("fakecookie", cookie, base64Encode: true);

			CookieFake cachedCookie = _target.Get<CookieFake>("fakecookie", true);

			Assert.IsNotNull(cachedCookie);
			Assert.AreEqual(cookie.TestProperty, cachedCookie.TestProperty);
			Assert.AreEqual(cookie.TestPropertyString, cachedCookie.TestPropertyString);
		}

		[TestMethod]
		public void CookieService_GetOrSetCookie_SetsCookie_Success()
		{
			Func<CookieFake> createCookie = () =>
			{
				return new CookieFake { TestProperty = 25, TestPropertyString = "blah" };
			};

			var cookie = _target.GetOrSet<CookieFake>("fakecookie", createCookie);
			Assert.IsNotNull(cookie);
			Assert.AreEqual(cookie.TestProperty, 25);
		}

		[TestMethod]
		public void CookieService_GetOrSetCookie_GetsCookie_Success()
		{
			CookieFake cookie = new CookieFake { TestProperty = 25, TestPropertyString = "blah" };
			_target.Set("fakecookie", cookie);

			Func<CookieFake> createCookie = () =>
			{
				return new CookieFake { TestProperty = 55, TestPropertyString = "blah2" };
			};

			var retrievedCookie = _target.GetOrSet<CookieFake>("fakecookie", createCookie);
			Assert.IsNotNull(retrievedCookie);
			Assert.AreEqual(retrievedCookie.TestProperty, cookie.TestProperty);
			Assert.AreEqual(retrievedCookie.TestPropertyString, cookie.TestPropertyString);
		}

		[TestMethod]
		public void CookieService_GetCookie_Fail()
		{
			CookieFake cachedCookie = _target.Get<CookieFake>("fakecookie");
			Assert.IsNull(cachedCookie);
		}

		[TestMethod]
		public void CookieService_GetCookie_Base64_Fail()
		{
			CookieFake cookie = new CookieFake { TestProperty = 25, TestPropertyString = "blah" };
			_target.Set("fakecookie", cookie);

			CookieFake cachedCookie = _target.Get<CookieFake>("fakecookie", true);
			Assert.IsNull(cachedCookie);
		}
	}

	public class CookieFake
	{
		public int TestProperty { get; set; }
		public string TestPropertyString { get; set; }
	}
}
