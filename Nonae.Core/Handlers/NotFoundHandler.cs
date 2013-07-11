﻿using System.Net.Http;
using Nonae.Core.Requests;
using Nonae.Core.Results;

namespace Nonae.Core.Handlers
{
	internal class NotFoundHandler : IHandler
	{
		private readonly IHandler _putHandler;

		public NotFoundHandler(IHandler putHandler)
		{
			_putHandler = putHandler;
		}

		public IResult Handle(IRequestDetails requestDetails)
		{
			return requestDetails.Answers(HttpMethod.Put) 
				? _putHandler.Handle(requestDetails) 
				: NotFoundResult.ForNonexistentResource();
		}
	}
}