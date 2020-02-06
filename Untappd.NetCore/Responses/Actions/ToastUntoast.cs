using System;
using System.Collections.Generic;
using RestSharp;
using Untappd.NetCore.Request;
namespace Untappd.NetCore.Responses.Actions
{
	public class ToastUntoast : IAction
	{
		public Method RequestMethod { get { return Method.POST; } }
		public IDictionary<string, object> BodyParameters { get { return new Dictionary<string, object>(); } }
		public string EndPoint { get; private set; }

		/// <summary>
		///
		/// </summary>
		/// <param name="checkinId"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public ToastUntoast(string checkinId)
		{
			if (string.IsNullOrWhiteSpace(checkinId))
			{
				throw new ArgumentNullException("checkinId");
			}
			EndPoint = string.Format("v4/checkin/toast/{0}", checkinId);
		}
	}
}
