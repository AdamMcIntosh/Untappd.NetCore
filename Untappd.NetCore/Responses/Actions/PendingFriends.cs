﻿using System.Collections.Generic;
using RestSharp;
using Untappd.NetCore.Request;

namespace Untappd.NetCore.Responses.Actions
{
    public class PendingFriends : IAction
	{
		public Method RequestMethod { get { return Method.Get; } }
		public string EndPoint { get { return "v4/user/pending"; } }

		public IDictionary<string, object> BodyParameters
		{
			get
			{
				var dict = new Dictionary<string, object>();
				if (Offset.HasValue)
				{
					dict.Add("offset", Offset.Value);
				}
				if (Limit.HasValue)
				{
					dict.Add("limit", Limit.Value);
				}
				return dict;
			}
		}

		public int? Offset { get; set; }
		public int? Limit { get; set; }
	}
}
