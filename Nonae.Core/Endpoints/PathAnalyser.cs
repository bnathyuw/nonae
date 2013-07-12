using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nonae.Core.Endpoints
{
	internal class PathAnalyser
	{
		private readonly string _url;

		public PathAnalyser(string url)
		{
			_url = url;
		}

		public Dictionary<string, string> GetAddressParts(string path)
		{
			if (path == null) return null;
			if (_url == null) return null;
			var pattern = new Regex(_url);
			var match = pattern.Match(path);

			var addressParts = new Dictionary<string, string>();
			var groupCollection = match.Groups;
			for (var i = 1; i <= groupCollection.Count; i++)
			{
				var key = pattern.GroupNameFromNumber(i);
				var value = groupCollection[i].Value;
				addressParts.Add(key, value);
			}
			return addressParts;
		}
	}
}