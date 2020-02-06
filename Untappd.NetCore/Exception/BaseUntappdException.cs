using System;
namespace Untappd.NetCore.Exception
{
	[Serializable]
	public class BaseUntappdException : System.Exception
	{
		public BaseUntappdException()
		{
		}

		public BaseUntappdException(string message) : base(message)
		{
		}

		public BaseUntappdException(string message, System.Exception inner) : base(message, inner)
		{
		}
	}
}
