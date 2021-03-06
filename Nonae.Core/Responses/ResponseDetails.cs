﻿using System.Net;
using System.Web;

namespace Nonae.Core.Responses
{
	public class ResponseDetails : IResponseDetails
	{
		private readonly HttpResponse _response;

		public ResponseDetails(HttpResponse response)
		{
			_response = response;
		}

		public HttpStatusCode StatusCode
		{
			set { _response.StatusCode = (int) value; }
		}

		public string WwwAuthenticate
		{
			set { _response.Headers.Add("WWW-Authenticate", value); }
		}

		public string Body
		{
			set { _response.Write(value); }
		}

		public string Allow
		{
			set { _response.Headers.Add("Allow", string.IsNullOrEmpty(value) ? " " : value); }
		}
	}
}