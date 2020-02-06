using System.Collections.Generic;

namespace Untappd.NetCore.Authentication
{
	public interface IUntappdCredentials
	{
		IReadOnlyDictionary<string, string> AuthenticationData { get; }
	}
}
