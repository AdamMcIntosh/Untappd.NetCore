using System.Collections.Generic;
using RestSharp;
using Untappd.NetCore.Request;

namespace Untappd.NetCore.Responses.Actions
{
    public class RemoveFromWishList : IAction
	{
		public Method RequestMethod { get { return Method.Get; } }
		public string EndPoint { get { return "v4/user/wishlist/delete"; } }
		public IDictionary<string, object> BodyParameters { get; private set; }

		public RemoveFromWishList(int beerId)
		{
			BodyParameters = new Dictionary<string, object>() { { "bid", beerId } };
		}
	}
}
