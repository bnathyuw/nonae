﻿using Nonae.Core.Handlers;

namespace Nonae.Core.Endpoints
{
	public interface IEndpoint
	{
		bool Allows(RequestDetails requestDetails);
		string AllowHeader { get; }
		bool Exists { get; }
	}
}