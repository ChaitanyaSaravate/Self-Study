using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TestMVCApp.Tests
{
	public class FakeControllerContext : ControllerContext
	{
		HttpContextBase context = new FakeHttpContext();

		public override HttpContextBase HttpContext
		{
			get => context;
			set => context = value;
		}
	}

	public class FakeHttpContext : HttpContextBase
	{
		HttpRequestBase _request = new FakeHttpRequest();

		public override HttpRequestBase Request
		{
			get { return _request; }
		}
	}

	public class FakeHttpRequest : HttpRequestBase
	{
		public override string this[string key]
		{
			get { return null; }
		}

		public override NameValueCollection Headers
		{
			get { return new NameValueCollection();}
		}
	}
}
