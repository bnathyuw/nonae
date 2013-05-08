﻿namespace Nonae.Tests.EndToEnd
{
	public class Context
	{
		private Response _response;
		private Request _request;

		public Response Response
		{
			get { return _response ?? (_response = _request.GetResponse()); }
		}

		public Request Request
		{
			set { _request = value; }
		}
	}
}