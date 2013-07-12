﻿using System.Net;
using Nonae.Core.Endpoints;
using Nonae.Core.Responses;

namespace Nonae.Core.Results
{
	internal class OptionsResult : IResult
	{
	    private readonly IEndpointDetails _endpointDetails;

	    public OptionsResult(IEndpointDetails endpointDetails)
		{
	        _endpointDetails = endpointDetails;
		}

	    public void Update(IResponseDetails responseDetails)
		{
			responseDetails.StatusCode = HttpStatusCode.OK;
            responseDetails.Allow = _endpointDetails.AllowHeader;
		}
	}
}