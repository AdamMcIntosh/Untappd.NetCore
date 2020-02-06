﻿using System;
using System.Collections.Generic;
using RestSharp;
using Untappd.NetCore.Request;

namespace Untappd.NetCore.Responses.Actions
{
	public class AcceptFriend : IAction
	{
		public Method RequestMethod { get { return Method.GET; } }
		public string EndPoint { get; private set; }
		public IDictionary<string, object> BodyParameters { get { return new Dictionary<string, object>(); } }

		public AcceptFriend(string target_id)
		{
			if (string.IsNullOrWhiteSpace(target_id))
			{
				throw new ArgumentNullException("target_id");
			}
			EndPoint = string.Format("v4/friend/accept/{0}", target_id);
		}
	}
}
