﻿using System.Net;
using Nonae.Core.Handlers;

namespace Nonae.Core.Results
{
	internal class OptionsResult : IResult
	{
		private readonly RequestDetails _requestDetails;

		public OptionsResult(RequestDetails requestDetails)
		{
			_requestDetails = requestDetails;
		}

		public void Update(ResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.OK;
			responseDetails.Allow = _requestDetails.AllowHeader;
		}
	}
}