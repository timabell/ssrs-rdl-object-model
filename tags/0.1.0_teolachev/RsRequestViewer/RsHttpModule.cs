using System;
using System.Web;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace RsRequestViewer
{
	/// <summary>
	/// An Http Handler that outputs the traffic to the Report Server.
	/// Both URL and SOAP requests are echoed.
	/// </summary>
	public class RsHttpModule : IHttpModule
	{

		public delegate void MyEventHandler(Object s, EventArgs e);

		public void Init(HttpApplication app)
		{
			// register event handler
			app.BeginRequest +=
				new EventHandler(this.OnBeginRequest);
		}
		public void Dispose() { }

		public void OnBeginRequest(object obj, EventArgs ea)
		{
			bool soapRequest = false;
			Stream stream = null;

			try
			{
				// check to see if request is a SOAP
				// message by looking for SOAPAction
				HttpApplication app = (HttpApplication)obj;
				HttpContext ctx = app.Context;

				soapRequest = (ctx.Request.Headers["SOAPAction"] != null);
				if (!soapRequest)
				{
					// URL request
					Trace.WriteLine(String.Format("RsHttpModule - URL request: {0}", ctx.Request.Url));
				}
				else
				{
					// SOAP request
					stream = ctx.Request.InputStream;
					byte[] requestBody = new byte[stream.Length];
					stream.Read(requestBody, 0, requestBody.Length);
					// set stream position to the beginning
					ctx.Request.InputStream.Position = 0;
					string request = System.Text.ASCIIEncoding.ASCII.GetString(requestBody);
                    Trace.WriteLine(String.Format("RsHttpModule - SOAP request: {0} from {1}", ctx.Request.Headers["SOAPAction"], ctx.Request.Url));
					Trace.WriteLine("RsHttpModule - SOAP payload: " + request);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(String.Format("RsHttpModule\n" + ex.ToString()));
			};

		}
	}
}
